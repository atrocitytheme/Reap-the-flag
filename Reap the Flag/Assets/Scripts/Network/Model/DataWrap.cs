using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWrap
{
    public GameObject gameObj;
    public TestModel model;
    /// <summary>
    /// Sync gameobj with model
    /// </summary>
    public void SyncGameObject() {
        WorldPoint pt = model.Location.Location;

        Vector3 newLocation = new Vector3((float)pt.X, (float)pt.Y, (float)pt.Z);
        gameObj.transform.position = newLocation;

        WorldPoint rt = model.Rotation.Rotation;

        Vector3 newRotation = new Vector3(rt.X, rt.Y, rt.Z);

        gameObj.transform.eulerAngles = newRotation;
    }

    // sync model with the gameobj
    public void SyncModel() {
        WorldPoint loc = new WorldPoint();
        WorldPoint pt = new WorldPoint();
        Vector3 l = gameObj.transform.position;
        Vector3 q = gameObj.transform.eulerAngles;
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
