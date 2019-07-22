using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainPlayerSpawnManager : PlayerSpawnManager
{
    private TestModel model = new TestModel();

    public TestModel Model {
        get {
            return model;
        }

        set {
            model = value;
        }
    }
    private void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer() {
        GameObject obj = Instantiate(player, Vector3.zero, Quaternion.identity);
        player = obj;
    }

    private bool playerExists() {
        return player != null;
    }
}
