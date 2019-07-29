﻿using PlayerComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameStateMachine : MonoBehaviour
{
    public MessageClient messageClient;
    public KetFrameClient ketFrameClient;
    private StateType state = StateType.NON_INITIALIZED;
    public PlayerSpawnManager spawnManager;
    public MainPlayerSpawnManager playerSpawnManager;
    public GameObject warningSign;
    public ObSpawnManager obManager;

    int networkTimeout = 0;
    public StateType State {
        get {
            return state;
        }
    }

    private string id;
    private string newName;
    private string password;



    private void Start()
    {
        state = StateType.NON_INITIALIZED;
        id = PlayerPrefs.GetString("id");
        password = PlayerPrefs.GetString("password");
        newName = PlayerPrefs.GetString("newName");
    }
    public void StartGame() {
        if (state == StateType.NON_INITIALIZED) state = StateType.INITIALIZED;
    }
    // call when tcp connection finished
    public void FinishConnect() {
        if (state == StateType.PENDING) {
            if (playerSpawnManager.PlayerExists())
            {
                state = StateType.INITIALIZED;
            }
            else {
                state = StateType.NON_INITIALIZED;
            }
        }
    }

    public void Pend() {
        state = StateType.PENDING;
    }
    private void FixedUpdate()
    {
        if (state == StateType.PENDING)
        {
            warningSign?.SetActive(true);
            /*Task.Run(()=> messageClient.Connect());*/
            Task.Run(() => ketFrameClient.Connect());
        }
        else {
            warningSign?.SetActive(false);
        }

        if (state == StateType.NON_INITIALIZED) {
            warningSign?.SetActive(true);
            TestModel generationCommand = ConfigureBasicModel();
            generationCommand.CommandType = 0;
          
            messageClient.AskForUpdate(generationCommand);
            
            
        }

        if (state == StateType.INITIALIZED) {
            playerSpawnManager.Player.SyncModel();
            TestModel m = playerSpawnManager.Player.model;
            m.CommandType = 1;
            m.Name = newName;
            m.Password = password;
            m.Id = id;
            messageClient.AskForUpdate(m);
        }

        if (state == StateType.DAMAGED){
            state = StateType.INITIALIZED;
        }

        if (state == StateType.KILLED) {
            playerSpawnManager.Player.gameObj.GetComponent<PlayerHealth>().Death();
            obManager.RegisterInfo(playerSpawnManager.Player.model);
            playerSpawnManager.Die();
            state = StateType.OB;
            ketFrameClient.AskForKeyFrame(new TestModel { CommandType=6, Name=newName, Password=password, Id=id});
        }

        if (state == StateType.OB) {
            TestModel m = playerSpawnManager.Player.model;
            m.CommandType = 10;
            m.Name = newName;
            m.Password = password;
            m.Id = id;
            messageClient.AskForUpdate(m);

            if (!obManager.Spawned()) {
                obManager.Spawn();
            }
        }
        if (networkTimeout <= 0)
        {
            CheckNetWork();
            networkTimeout = 100;
        }
        networkTimeout -= 1;
    }

    private void CheckNetWork() {
        /*if (messageClient.TestTcpConnection())
        messageClient.AskForKeyFrame(new TestModel { CommandType=101});*/
        if (ketFrameClient.TestTcpConnection()) {
            ketFrameClient.AskForKeyFrame(new TestModel { CommandType = 101, Id=id, Name = newName, Password=password});
        }
    }

    public void StopGame() {
        state = StateType.IDLE;
    }
    // configure basic model with passsword, id and newName
    private TestModel ConfigureBasicModel() {
        return new TestModel
        {
            CommandType = 0,
            Id = id,
            Password = password,
            Name = newName
        };
    }

    public void DeathState() {
        if (state != StateType.NON_INITIALIZED || 
            state != StateType.IDLE || 
            state != StateType.KILLED) {
            state = StateType.KILLED;
        }
    }
}