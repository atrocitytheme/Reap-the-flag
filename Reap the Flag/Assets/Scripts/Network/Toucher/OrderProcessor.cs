using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderProcessor : MonoBehaviour
{
    public GameObject facade;
    public GameStateMachine starter;
    private MessageClient messageClient;
    private GameStateMachine stateMachine;
    public Dictionary<int, GameObject> handlers;
    private MainPlayerSpawnManager playerSpawnManager;
    private PlayerSpawnManager spawnManager;
    private PositionAdjuster adjuster;
    private void Awake()
    {
        messageClient = GetComponent<MessageClient>();
        stateMachine = GetComponentInChildren<GameStateMachine>();
    }
    private void Start()
    {
        playerSpawnManager = facade.GetComponent<MainPlayerSpawnManager>();
        spawnManager = facade.GetComponent<PlayerSpawnManager>();
        adjuster = facade.GetComponent<PositionAdjuster>();
    }
    public void Process(string json) {
        Debug.Log(json);
        if (stateMachine.State == StateType.NON_INITIALIZED)
        {
            JArray result = JArray.Parse(json.Trim());
            foreach (JObject obj in result)
            {
                var id = obj["Id"].ToObject<string>();
                TestModel newModel = ProcessModel(obj);
                if (id == PlayerPrefs.GetString("id"))
                {
                    Debug.Log("me!");
                    playerSpawnManager.SpawnPlayer(newModel);
                }

                else
                {
                    spawnManager.SpawnPlayer(newModel);
                }

            }

            starter.StartGame();
        }
        else if (stateMachine.State == StateType.INITIALIZED) {

        }
    }


    private TestModel InitPlayerModel()
    {
        return new TestModel
        {
            CommandType = 0,
            Location = new WorldLocation(),
            Rotation = new WorldRotation(),
        };
    }

    private TestModel ProcessModel(JObject obj) {
        var id = obj["Id"].ToObject<string>();
        var token = obj["Token"].ToObject<string>();
        var lo = obj["Location"].ToObject<WorldLocation>();
        var commandType = obj["CommandType"].ToObject<int>();
        var rr = new WorldRotation();

        rr.Rotation = obj["Rotation"].ToObject<WorldPoint>();

        TestModel newModel = InitPlayerModel();
        newModel.Id = id;
        newModel.RoomId = obj["RoomId"].ToObject<int>();
        Debug.Log(rr.Rotation);
        newModel.Rotation = rr;
        newModel.Location = lo;

        return newModel;
    }
}
