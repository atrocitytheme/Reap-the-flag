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
public class NetworkReceiver : MonoBehaviour
{
    public void registerClient(UdpClient client) {
        client.BeginReceive(new AsyncCallback(recv(client)), client);
    }

    private Action<IAsyncResult> recv(UdpClient client) {

        return (IAsyncResult res) => {
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 9956);
            Byte[] received = client.EndReceive(res, ref remotePoint);
            Debug.Log("got: " + Encoding.UTF8.GetString(received));
            registerClient(client);
        };
    }
}
