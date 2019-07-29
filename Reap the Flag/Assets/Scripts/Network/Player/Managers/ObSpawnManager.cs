using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSpawnManager : MonoBehaviour
{
    public GameObject OB_Prototype;
    public DataWrap currentOb;
    private bool spawned = false;
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Spawned()) {
            currentOb.SyncModel();
        }
    }
    public bool Spawned() {
        return spawned;
    }

    public void Spawn(TestModel model) {
        DataWrap wrap = new DataWrap();
        Vector3 v3 = new Vector3(model.Location.Location.X, model.Location.Location.Y, model.Location.Location.Z);

        GameObject obj = Instantiate(OB_Prototype, v3, Quaternion.identity);
        wrap.model = model;
        wrap.gameObj = obj;
        currentOb = wrap;
        spawned = true;
        Debug.Log("Spawned!");
        currentOb.SyncModel();
    }

    public void Spawn() {
        if (currentOb.model != null)
        {
            Spawn(currentOb.model);
        }

        else {
            Debug.LogError("You must register model info before spawn ob!");
        }
    }

    public void RegisterInfo(TestModel model) {
        DataWrap wrap = new DataWrap();
        wrap.model = model;
        currentOb = wrap;
    }
}
