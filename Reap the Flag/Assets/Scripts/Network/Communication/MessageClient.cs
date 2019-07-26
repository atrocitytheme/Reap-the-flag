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
    private bool connecting = false;
    public GameStateMachine stateMachine;
    UdpClient udpClient = new UdpClient();
    TcpClient tcpClient = new TcpClient();
    NetworkReceiver receiver;
    private IPEndPoint remoteEP;
    private void Awake()
    {
        receiver = GetComponent<NetworkReceiver>();
        remoteEP = new IPEndPoint(IPAddress.Parse(ip), 9956);
        tcpClient.ReceiveTimeout = 5000;
    }

    private void Start()
    {
       
    }

    public async Task Connect() {
        Debug.Log("connecting......");
        if (connecting) return;
        connecting = true;
        Debug.Log("proceeding connection...");
        await tcpClient.ConnectAsync(ip, port);
        receiver.QueueMainThreadWork(() =>
        {
            if (tcpClient.Connected)
            {
                Debug.Log("Tcp connected!");
                stateMachine?.FinishConnect();
            }
            connecting = false;
        });

    }

    private void Update()
    {
        /*if (!tcpClient.Connected)
        {
            stateMachine?.Pend();
            tcpClient = new TcpClient();
            connecting = false;
        }*/
    }
    public void AskForUpdate(TestModel modelMessage) {
        SendData(JsonConvert.SerializeObject(modelMessage), udpClient);
    }

    private void SendData(string dataSent, UdpClient cli) {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        cli.Send(data, data.Length, remoteEP);
        receiver.ProcessMessage(cli);
    }

    public void AskForKeyFrame(TestModel modelMessage) {
        SendKeyFrame(JsonConvert.SerializeObject(modelMessage), tcpClient);
    }

    public void SendKeyFrame(string dataSent, TcpClient client) {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        NetworkStream stream = client.GetStream();
        if (stream.CanWrite) {
            stream.WriteAsync(data, 0, data.Length);
            receiver.ProcessTcpMessage(client, data.Length);
        }
    }

    private void OnDestroy()
    {
        tcpClient.Close();
    }

    public bool TestTcpConnection(TcpClient client) {
        if (client == null) return false;
        return client.Connected;
    }

    public bool TestTcpConnection() {
        return TestTcpConnection(tcpClient);
    }
}
