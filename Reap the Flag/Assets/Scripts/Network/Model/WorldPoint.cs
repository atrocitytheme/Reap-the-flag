using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPoint
{
    // Start is called before the first frame update
    float x, y, z;

    public float X {
        get {
            return x;
        }

        set {
            x = value;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public float Z
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
        }
    }
    public override string ToString()
    {
        return "x: " + x +
            "y: " + y +
            "z: " + z;
    }
}
