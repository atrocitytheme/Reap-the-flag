using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    public GameObject player;
    private void Start()
    {
        SpawnPlayer();
    }

    private void FixedUpdate()
    {
    }
    public void SpawnPlayer() {
        GameObject obj = Instantiate(player, Vector3.zero, Quaternion.identity);
    }
}
