using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainPlayerSpawnManager : PlayerSpawnManager
{
    private string id = "null";
    private DataWrap curPlayer;

    public string Id {
        get {
            return id;
        }
    }

    public DataWrap Player {
        get {
            return curPlayer;
        }
    }

    public TestModel Model {
        get {
            return curPlayer.model;
        }

        set {
            curPlayer.model = value;
        }
    }

    public override void SpawnPlayer(TestModel md) {
        GameObject obj = Instantiate(player, Vector3.zero, Quaternion.identity);
        curPlayer = new DataWrap { gameObj = obj, model = md };
        this.id = curPlayer.model.Id;
    }

    private bool playerExists() {
        return player != null;
    }

    public void DeleteCurPlayer() {
        Destroy(curPlayer.gameObj);
        curPlayer = null;
    }


    public TestModel RetrieveModel(int commandType) {
        TestModel model = new TestModel {
            CommandType = commandType,
            Id = id,
            RoomId = curPlayer.model.RoomId
        };

        return model;
    }
}
