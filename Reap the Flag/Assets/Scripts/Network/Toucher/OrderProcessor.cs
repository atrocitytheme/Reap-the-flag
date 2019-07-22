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
            var commandType = obj["CommandType"].ToObject<int>();
            Debug.Log("command: " + commandType);
            Debug.Log(stateMachine.State);
            if (commandType == 0 && stateMachine.State == StateType.NON_INITIALIZED) {
                Debug.Log("game started!");
                starter.StartGame();
            }

            if (commandType == 1) {
                if (id.Equals(playerSpawnManager.Id))
                {
                    
                }

                else
                {
                    var rotation = obj["Rotation"];
                    var rot = ExtractRotation(rotation);

                    GameObject prop;
                    if ((prop = spawnManager.RetrievePlayer(id)))
                    {
                        prop.transform.rotation = rot;
                    }

                }
            }
        }
    }


    private Vector3 ExtractPosition(JToken token) {

        Vector3 newPos = new Vector3();
        
        return newPos;
    }

    private Quaternion ExtractRotation(JToken token) {
        WorldRotation loc = token.ToObject<WorldRotation>();
        WorldPoint pt = loc.Rotation;
        Vector3 newRot = new Vector3((float)pt.X, (float)pt.Y, (float)pt.Z);
        return Quaternion.Euler(newRot);
        
    }

}
