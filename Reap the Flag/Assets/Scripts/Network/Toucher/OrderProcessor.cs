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
        if (stateMachine.State == StateType.NON_INITIALIZED)
        {
            foreach (JObject obj in result)
            {
                var id = obj["Id"].ToObject<string>();
                TestModel newModel = ProcessModel(obj);
                if (id == PlayerPrefs.GetString("id"))
                {
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
            foreach (JObject obj in result) {

                TestModel model = ProcessModel(obj);

                if (playerSpawnManager.IsPlayer(model)) continue;
                // check if current player has been spawned or not
                if (model.CommandType == 0 && 
                    !spawnManager.Exists(model) 
                    ) {
                    spawnManager.SpawnPlayer(model);
                }

                else if (model.CommandType == 1) {
                    spawnManager.RetrievePlayer(model).SyncGameObject();
                }
            }
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
        newModel.CommandType = commandType;
        newModel.Id = id;
        newModel.RoomId = obj["RoomId"].ToObject<int>();
        newModel.Rotation = rr;
        newModel.Location = lo;

        return newModel;
    }
}
