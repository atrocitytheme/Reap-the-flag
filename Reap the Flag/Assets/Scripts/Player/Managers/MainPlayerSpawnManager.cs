using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerSpawnManager : PlayerSpawnManager
{
    private void Start()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer() {
        GameObject obj = Instantiate(player, Vector3.zero, Quaternion.identity);
    }
}
