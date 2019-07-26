using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldLocation
{
    WorldPoint location;
    public WorldPoint Location {
        get {
            return location;
        }

        set {
            location = value;
        }
    }
    
}
