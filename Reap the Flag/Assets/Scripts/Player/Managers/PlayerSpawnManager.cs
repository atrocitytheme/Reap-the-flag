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
    public void SpawnPlayer()
    {
        GameObject obj = Instantiate(player, new Vector3(0, 5, 0), Quaternion.identity);
    }

}
