using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// register the listening event for the client and process any async events
/// </summary>
[RequireComponent(typeof(OrderProcessor))]
public class NetworkReceiver : MonoBehaviour { 
    private static readonly Queue<Action> tasks = new Queue<Action>();
    private OrderProcessor processor;
    private bool tcpOpen = false;
    private bool udpOpen = false;

    private void Awake()
    {
        processor = GetComponent<OrderProcessor>();
    }
    private void Update()
    {
        this.HandleTasks();
    }

    void HandleTasks() {
        while (tasks.Count > 0) {
            Action task = null;

            lock (tasks) {
                if (tasks.Count > 0) {
                    task = tasks.Dequeue();
                }
            }

            task();
        }
    }

    public void ProcessMessage(UdpClient curClient) {
        
        curClient.BeginReceive(new AsyncCallback(recv(curClient)), curClient);
        udpOpen = true;   
    }

    private Action<IAsyncResult> recv(UdpClient client) {

        return (IAsyncResult res) => {
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            Byte[] received = client.EndReceive(res, ref remotePoint);
            QueueMainThreadWork(() => {
                processor.Process(Encoding.UTF8.GetString(received));
            });
        };
    }

    public void QueueMainThreadWork(Action task) {
        lock (tasks) {
            tasks.Enqueue(task);
        }
    }

    public void ProcessTcpMessage(TcpClient client, int tcpLength)
    {
        if (!tcpOpen) // since it's keep-alive connection
        {
            Debug.Log("listening pool established!");
            tcpOpen = true;
            Task.Run(()=> AsyncRead(client, tcpLength));
        }
    }

    private async Task AsyncRead(TcpClient client, int tcpLength) {
        Byte[] received = new byte[200];
        NetworkStream stream = client.GetStream();
        int total = 0;
        int length = int.MaxValue;
        bool lengthSet = false;
        while (true)
        {
            try
            {
                int dataToRead = lengthSet ? length + 4 - total : 4;
                var curBytes = await stream.ReadAsync(received, total, dataToRead);
                total += curBytes;

                if (!lengthSet)
                {
                    if (total >= 4)
                    {
                        Byte[] arr = new byte[4];
                        Array.Copy(received, 0, arr, 0, 4);
                        length = BitConverter.ToInt32(arr, 0);
                        lengthSet = true;
                        QueueMainThreadWork(() =>
                        {
                            Debug.Log("length is " + length);
                        });
                    }
                }
                else {
                    if (total - 4 >= length) {
                        Byte[] newArr = new byte[length];
                        Array.Copy(received, 4, newArr, 0, length);
                        QueueMainThreadWork(() =>
                        {
                            Debug.Log("content: ..." + Encoding.UTF8.GetString(newArr));
                            processor.ProcessTcp(Encoding.UTF8.GetString(newArr));
                        });
                        lengthSet = false;
                        total = 0;
                        length = int.MaxValue;
                        received = new byte[200];
                    }
                }
            }
            catch (Exception)
            {
                tcpOpen = false;
                return;
            }
        }
    }

    public void UpdateReceiver() {
        tcpOpen = false;
    }
}
