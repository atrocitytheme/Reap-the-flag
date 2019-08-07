using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkReceiver))]
public class MessageClient : MonoBehaviour
{
    public string ip;
    public int port;
    public GameStateMachine stateMachine;
    UdpClient udpClient = new UdpClient();
    TcpClient tcpClient = new TcpClient();
    NetworkReceiver receiver;
    private IPEndPoint remoteEP;

    private void Start()
    {
        receiver = GetComponent<NetworkReceiver>();
        remoteEP = new IPEndPoint(IPAddress.Parse(ip), 9956);
        tcpClient.ReceiveTimeout = 50000;
        udpClient.Client.ReceiveTimeout = 50000;
        udpClient = new UdpClient();
    }

   

    public void AskForUpdate(TestModel modelMessage) {
        SendData(JsonConvert.SerializeObject(modelMessage), udpClient);
    }

    private void SendData(string dataSent, UdpClient cli) {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        cli.Send(data, data.Length, remoteEP);
        receiver.ProcessMessage(cli);
    }


    private void OnDestroy()
    {
        udpClient.Close();
    }
}
