using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderProcessor : MonoBehaviour
{
    public GameObject facade;
    private MainPlayerSpawnManager playerSpawnManager;
    private PlayerSpawnManager spawnManager;
    private PositionAdjuster adjuster;

    private void Start()
    {
        playerSpawnManager = facade.GetComponent<MainPlayerSpawnManager>();
        spawnManager = facade.GetComponent<PlayerSpawnManager>();
        adjuster = facade.GetComponent<PositionAdjuster>();
    }
    public void process(string json) {

        Debug.Log(json);
        JArray result = JArray.Parse(json.Trim());
        Debug.Log("processe finished!");
    }
}
