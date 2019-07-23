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
        JArray result = JArray.Parse(json.Trim());
        foreach (JObject obj in result) {
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

            Debug.Log("command: " + commandType);
            if (commandType == 0 && stateMachine.State == StateType.NON_INITIALIZED) {
                Debug.Log("game started!");
                
                if (token == "me")
                {
                    Debug.Log("me!");
                    playerSpawnManager.SpawnPlayer(newModel);
                }

                else {
                    Debug.Log("other!");
                    spawnManager.SpawnPlayer(newModel);
                }
            }

            if (commandType == 1) {
                              
            }
        }

        starter.StartGame();
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

}
