using PlayerComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class ExitController : MonoBehaviour
{
    public GameObject UI_Board;
    bool isExiting = false;
    GameStateMachine machine;
    PlayerController controller;
    private void Start()
    {
        machine = GameObject.Find("/NetworkTesting/Communicator").GetComponent<GameStateMachine>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isExiting)
            {
                controller.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                DownCurve();
            }

            else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UpCurve();
                controller.enabled = true;
            }
        }
    }

    public void DownCurve() {
        UI_Board.SetActive(true);
    }

    public void UpCurve() {
        UI_Board.SetActive(false);
    }

    public void Exit() {
        machine.State = StateType.EXIT;
    }
}
