using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModel
{
    private string ip;
    private int commandType;

    public string IP {
        get {
            return ip;
        }

        set {
            ip = value;
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
}
