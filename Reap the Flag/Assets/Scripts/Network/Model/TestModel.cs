using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModel
{
    private string ip = System.Net.Dns.GetHostName();
    private int commandType;
    private int roomId = 1;
    private int port = 5000;

    public string Ip {
        get {
            return ip;
        }

        set {
            ip = value;
        }
    }

    public int RoomId {
        get {
            return roomId;
        }

        set {
            roomId = value;
        }
    }

    public int CommandType {
        get {
            return commandType;
        }

        set {
            commandType = value;
        }
    }

    public int Port {
        get {
            return port;
        }

        set {
            port = value;
        }
    }
}
