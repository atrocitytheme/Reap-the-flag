using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public MessageClient messageClient;
    private StateType state = StateType.NON_INITIALIZED;

    public StateType State {
        get {
            return state;
        }
    }
    private void Start()
    {
        if (messageClient)
        {
            /*StartGame();*/
        }

        else {
            Debug.LogError("should install messageClient before start his game");
        }
    }
    public void StartGame() {
        state = StateType.INITIALIZED;
    }
    private void FixedUpdate()
    {
        if (state == StateType.NON_INITIALIZED) {
            messageClient.AskForUpdate(new TestModel { CommandType = 0});
        }

        if (state == StateType.INITIALIZED) {
            messageClient.AskForUpdate(new TestModel { CommandType=1, RoomId = 1});
        }
    }
    public void StopGame() {
        state = StateType.IDLE;
    }
}
