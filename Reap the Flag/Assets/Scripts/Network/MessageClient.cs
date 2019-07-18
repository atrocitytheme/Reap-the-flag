using LiteNetLib;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;

public class MessageClient : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("start!");
        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager client = new NetManager(listener);
        client.Start();
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string ip = "";

        foreach (IPAddress curIp in host.AddressList) {
            if (curIp.AddressFamily == AddressFamily.InterNetwork) {
                ip = curIp.ToString();
                break;
            }
        }

        TestModel model = new TestModel { Ip=ip, CommandType=1};
        Debug.Log("host: " + System.Net.Dns.GetHostName());
        client.Connect("10.7.8.185", 9956, JsonConvert.SerializeObject(model));
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            Debug.Log("we got: {0} " + dataReader.GetString(100));
        };

        Debug.Log(100 >> 8);
    }
    private void FixedUpdate()
    {
        
    }
}
