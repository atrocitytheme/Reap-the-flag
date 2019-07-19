using LiteNetLib;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class MessageClient : MonoBehaviour
{
    TestModel model = new TestModel {CommandType = 1 };
    UdpClient udpClient;
    private static IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("10.7.8.185"), 9956);
    private void Start()
    {
        string message = JsonConvert.SerializeObject(model);
        udpClient = new UdpClient();
        Byte[] data = Encoding.UTF8.GetBytes(message);
        GetComponent<NetworkReceiver>().registerClient(udpClient);
    }

    private void FixedUpdate()
    {
        askForUpdate();
    }

    private void askForUpdate() {
        sendData(JsonConvert.SerializeObject(model), udpClient);
    }

    private void sendData(string dataSent, UdpClient cli) {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        cli.Send(data, data.Length, remoteEP);
    }
}
