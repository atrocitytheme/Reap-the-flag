using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
/// <summary>
/// register the listening event for the client
/// </summary>
public class NetworkReceiver : MonoBehaviour
{
    public void registerClient(UdpClient client) {
        client.BeginReceive(new AsyncCallback(recv(client)), null);    
    }

    private Action<IAsyncResult> recv(UdpClient client) {

        return (IAsyncResult res) => {
            Debug.Log("received!");
            // RECEIVE FROM THE REOMOTE
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 9956);
            Byte[] received = client.EndReceive(res, ref remotePoint);
            registerClient(client);
        };
    }
}
