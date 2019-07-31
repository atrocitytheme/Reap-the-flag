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
    PlayerShooting shootingSystem;
    private void Start()
    {
        machine = GameObject.Find("/NetworkTesting/Communicator").GetComponent<GameStateMachine>();
        controller = GetComponent<PlayerController>();
        shootingSystem = GetComponentInChildren<PlayerShooting>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isExiting)
            {
                controller.enabled = false;
                shootingSystem.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                DownCurve();
                isExiting = true;
            }

            else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UpCurve();
                controller.enabled = true;
                shootingSystem.enabled = true;
                isExiting = false;
            }
        }
    }

    public void DownCurve() {
        UI_Board.GetComponent<Animator>().SetBool("shouldClip", true);
        UI_Board.SetActive(true);
    }

    public void UpCurve() {
        UI_Board.GetComponent<Animator>().SetBool("shouldClip", false);
        UI_Board.SetActive(false);
    }

    public void Exit() {
        machine.State = StateType.EXIT;
    }
}
