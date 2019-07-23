using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public MessageClient messageClient;
    private StateType state;
    public PlayerSpawnManager spawnManager;
    public MainPlayerSpawnManager playerSpawnManager;
    public StateType State {
        get {
            return state;
        }
    }


    private void Start()
    {
        state = StateType.NON_INITIALIZED;
    }
    public void StartGame() {
        if (state == StateType.NON_INITIALIZED) state = StateType.INITIALIZED;
    }
    private void FixedUpdate()
    {
        Debug.Log(State);
        if (state == StateType.NON_INITIALIZED) {
            messageClient.AskForUpdate(new TestModel { CommandType = 0});
        }

        if (state == StateType.INITIALIZED) {
            playerSpawnManager.Player.Sync();
            TestModel m = playerSpawnManager.Player.model;
            m.CommandType = 1;
            messageClient.AskForUpdate(m);
        }
    }
    public void StopGame() {
        state = StateType.IDLE;
    }
}
