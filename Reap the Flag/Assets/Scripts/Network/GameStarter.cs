using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public MessageClient messageClient;
    private StateType state = StateType.NON_INITIALIZED;
    private void Start()
    {
        if (messageClient)
        {
            StartGame();
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
            
        }

        if (state == StateType.INITIALIZED) {
            messageClient.AskForUpdate(new TestModel { CommandType=1, RoomId = 1});
        }
    }
    public void StopGame() {
        state = StateType.IDLE;
    }
}
