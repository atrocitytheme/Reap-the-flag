using Newtonsoft.Json.Linq;
using System;
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
        else if (stateMachine.State == StateType.INITIALIZED || stateMachine.State == StateType.OB)
        {
            foreach (JObject obj in result)
            {

                TestModel model = ProcessModel(obj);

                if (playerSpawnManager.IsPlayer(model)) continue;
                // check if current player has been spawned or not
                if (model.CommandType == 0 &&
                    !spawnManager.Exists(model)
                    )
                {
                    spawnManager.SpawnPlayer(model);
                }

                else if (model.CommandType == 1)
                {
                    SyncOnlinePlayers(model);
                }

                else if (model.CommandType == 10) {
                    SyncOnlinePlayers(model);
                }
            }
        }
    }

    public void ProcessTcp(string json) {
        Debug.Log(json);
        JObject frame = JObject.Parse(json);
        var commandType = frame["CommandType"].ToObject<int>();
        var target = frame["Target"].ToObject<string>();
        if (commandType == 6) {
            var player = spawnManager.RetrievePlayer(target);

            if (player != null) {
                player.gameObj.GetComponent<OnlinePlayerHealth>().Death();
                StartCoroutine(LaterAction(()=> {
                    Debug.Log("player dead!");
                    spawnManager.DeletePlayer(target);
                }));
            }
        }

        if (commandType == 11) {
            var player = spawnManager.RetrievePlayer(target);
            if (player != null) {
                spawnManager.DeletePlayer(target);
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
        var isShooting = obj["IsShooting"].ToObject<bool>();
        var isDead = obj["IsDead"].ToObject<bool>();
        var rr = new WorldRotation();

        rr.Rotation = obj["Rotation"].ToObject<WorldPoint>();

        TestModel newModel = InitPlayerModel();
        newModel.CommandType = commandType;
        newModel.Id = id;
        newModel.RoomId = obj["RoomId"].ToObject<int>();
        newModel.Rotation = rr;
        newModel.Location = lo;
        newModel.IsShooting = isShooting;
        newModel.IsDead = isDead;

        return newModel;
    }
    // sync movement of specific online player
    private void SyncOnlinePlayers(TestModel model) {
        DataWrap wrp = spawnManager.RetrievePlayer(model);
        if (wrp == null) return;
        wrp.model.Location = model.Location;
        wrp.model.Rotation = model.Rotation;
        wrp.model.IsShooting = model.IsShooting;
        wrp.SyncGameObject();
    }

    private IEnumerator LaterAction(Action act) {

        yield return new WaitForSeconds(10);
        act();
    }
}
