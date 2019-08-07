using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObExit : MonoBehaviour
{
    GameStateMachine machine;

    private void Start()
    {
        machine = GameObject.Find("/NetworkTesting/Communicator").GetComponent<GameStateMachine>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    public void Exit() {
        machine.State = StateType.EXIT;
    }
}
