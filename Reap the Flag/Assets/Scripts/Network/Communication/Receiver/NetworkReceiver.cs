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
    }

    private Action<IAsyncResult> recv(UdpClient client) {

        return (IAsyncResult res) => {
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 9956);
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
            AsyncRead(client, tcpLength);
        }
    }

    private async Task AsyncRead(TcpClient client, int tcpLength) {
        Byte[] received = new byte[tcpLength];
        NetworkStream stream = client.GetStream();
        int total = 0;
        int offset = 0;
        while (true)
        {
            try
            {
                var curBytes = await stream.ReadAsync(received, offset, tcpLength - total);
                total += curBytes;
                offset += curBytes;
               /* if (total > tcpLength)
                {*/
                    QueueMainThreadWork(() =>
                    {
                        Debug.Log("current bytes is: " + curBytes);
                        Debug.Log("ready for commence...");
                        processor.ProcessTcp(Encoding.UTF8.GetString(received));
                    });
                    offset = 0;
                    total = 0;
                /*}*/
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
