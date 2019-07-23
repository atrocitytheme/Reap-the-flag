using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWrap
{
    public GameObject gameObj;
    public TestModel model;
    // sync gameObj data with the model
    public void Sync() {
        WorldPoint loc = new WorldPoint();
        WorldPoint pt = new WorldPoint();
        Vector3 l = gameObj.transform.position;
        Quaternion q = gameObj.transform.rotation;
        loc.X = l.x;
        loc.Y = l.y;
        loc.Z = l.z;

        pt.X = q.x;
        pt.Y = q.y;
        pt.Z = q.z;
        model.Location.Location = loc;
        model.Rotation.Rotation = pt;
    }
}
