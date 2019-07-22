using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPoint
{
    // Start is called before the first frame update
    double x, y, z;

    public double X {
        get {
            return x;
        }

        set {
            x = value;
        }
    }

    public double Y
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

    public double Z
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
}
