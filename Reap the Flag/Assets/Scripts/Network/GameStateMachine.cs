using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public MessageClient messageClient;
    private StateType state = StateType.NON_INITIALIZED;
    public PlayerSpawnManager spawnManager;
    public MainPlayerSpawnManager playerSpawnManager;
    public StateType State {
        get {
            return state;
        }
    }

    private string id;
    private string name;
    private string password;



    private void Start()
    {
        state = StateType.NON_INITIALIZED;
        id = PlayerPrefs.GetString("id");
        password = PlayerPrefs.GetString("password");
        name = PlayerPrefs.GetString("name");

    }
    public void StartGame() {
        if (state == StateType.NON_INITIALIZED) state = StateType.INITIALIZED;
    }
    private void FixedUpdate()
    {
        if (state == StateType.NON_INITIALIZED) {
            TestModel generationCommand = ConfigureBasicModel();
            generationCommand.CommandType = 0;
            messageClient.AskForUpdate(generationCommand);
        }

        if (state == StateType.INITIALIZED) {
            playerSpawnManager.Player.Sync();
            TestModel m = playerSpawnManager.Player.model;
            m.CommandType = 1;
            m.Name = name;
            m.Password = password;
            m.Id = id;
            messageClient.AskForUpdate(m);
        }
    }
    public void StopGame() {
        state = StateType.IDLE;
    }
    // configure basic model with passsword, id and name
    private TestModel ConfigureBasicModel() {
        return new TestModel
        {
            CommandType = 0,
            Id = id,
            Password = password,
            Name = name
        };
    }
}
