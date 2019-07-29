using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KetFrameClient : MonoBehaviour
{
    // Start is called before the first frame update
    public string ip;
    public int port;
    private bool connecting = false;
    public GameStateMachine stateMachine;
    TcpClient tcpClient = new TcpClient();

    NetworkReceiver receiver;
    private IPEndPoint remoteEP;

    // Update is called once per frame
    private void Awake()
    {
        receiver = GetComponent<NetworkReceiver>();
        remoteEP = new IPEndPoint(IPAddress.Parse(ip), 9956);
        tcpClient.ReceiveTimeout = 5000;
    }

    public async Task Connect()
    {
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

    public void AskForKeyFrame(TestModel modelMessage)
    {
        SendKeyFrame(JsonConvert.SerializeObject(modelMessage), tcpClient);
    }

    public void SendKeyFrame(string dataSent, TcpClient client)
    {
        Byte[] data = Encoding.UTF8.GetBytes(dataSent);
        NetworkStream stream = client.GetStream();
        if (stream.CanWrite)
        {
            stream.WriteAsync(data, 0, data.Length);
            receiver.ProcessTcpMessage(client, data.Length);
        }
    }

    private void Update()
    {
        if (!tcpClient.Connected)
        {
            stateMachine?.Pend();
            tcpClient = new TcpClient();
            connecting = false;
        }
    }

    private void OnDestroy()
    {
        tcpClient.Close();
    }
    public bool TestTcpConnection(TcpClient client)
    {
        if (client == null) return false;
        return client.Connected;
    }

    public bool TestTcpConnection()
    {
        return TestTcpConnection(tcpClient);
    }
}
