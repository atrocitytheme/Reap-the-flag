using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading.Tasks;

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
        remoteEP = new IPEndPoint(IPAddress.Parse(ip), port);
        tcpClient.ReceiveTimeout = 5000;
    }

    public async Task Connect() {
        if (connecting) return;
        connecting = true;
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
        Debug.Log(tcpClient.Connected);
        if (!tcpClient.Connected)
        {
            stateMachine?.Pend();
        }
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
        
    }

    public void SendKeyFrame(string dataSent, TcpClient client) {

    }

    private void OnDestroy()
    {
        tcpClient.Close();
    }

    public bool TestTcpConnection(TcpClient client) {
        if (client.Client.Poll(0, SelectMode.SelectRead)) {
            byte[] buff = new byte[1];

            if (client.Client.Receive(buff, SocketFlags.Peek) == 0) {
                return false;
            }

            return true;
        }

        return true;
    }
}
