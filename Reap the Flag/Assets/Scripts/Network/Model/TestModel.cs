using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestModel
{
    private string ip = System.Net.Dns.GetHostName();
    private int commandType;
    private int roomId = 1;
    private int port = 5000;
    private string id;

    private WorldLocation location;
    private WorldRotation rotation;
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

    public WorldLocation Location {
        get {
            return location;
        }

        set {
            location = value;
        }
    }

    public WorldRotation Rotation {
        get {
            return rotation;
        }

        set {
            rotation = value;
        }
    }

    public string Id {
        get {
            return id;
        }

        set {
            id = value;
        }
    }
}
