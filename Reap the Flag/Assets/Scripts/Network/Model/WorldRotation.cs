using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation
{
    // Start is called before the first frame update
    WorldPoint point;

    public WorldPoint Rotation
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
