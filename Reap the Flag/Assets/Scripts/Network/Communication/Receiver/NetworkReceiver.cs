using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
/// <summary>
/// register the listening event for the client
/// </summary>
[RequireComponent(typeof(OrderProcessor))]
public class NetworkReceiver : MonoBehaviour
{
    private static readonly Queue<Action> tasks = new Queue<Action>();
    private OrderProcessor processor;

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
            string order = Encoding.UTF8.GetString(received);
            QueueMainThreadWork(() => {
/*                Debug.Log("got: " + Encoding.UTF8.GetString(received));
*/                processor.process(Encoding.UTF8.GetString(received));
            });
        };
    }

    public void QueueMainThreadWork(Action task) {
        lock (tasks) {
            tasks.Enqueue(task);
        }
    }
}
