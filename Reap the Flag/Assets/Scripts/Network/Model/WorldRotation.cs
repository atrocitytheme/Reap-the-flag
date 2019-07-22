using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation
{
    // Start is called before the first frame update
    Vector3 point;

    public Vector3 Rotation
    {
        get
        {
            return point;
        }

        set
        {
            point = value;
        }
    }
}
