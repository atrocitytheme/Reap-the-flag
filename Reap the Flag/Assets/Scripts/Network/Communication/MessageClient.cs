using LiteNetLib;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

[RequireComponent(typeof(NetworkReceiver))]
public class MessageClient : MonoBehaviour
{

    UdpClient udpClient = new UdpClient();
    TcpClient tcpClient;
    NetworkReceiver receiver;
    private static IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("10.7.8.185"), 9956);
    private void Awake()
    {
        receiver = GetComponent<NetworkReceiver>();
    }

    public void AskForUpdate(TestModel modelMessage) {
        SendData(JsonConvert.SerializeObject(modelMessage), udpClient);
    }

    private void SendData(string dataSent, UdpClient cli) {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        cli.Send(data, data.Length, remoteEP);
        receiver.ProcessMessage(cli);
    }
}
