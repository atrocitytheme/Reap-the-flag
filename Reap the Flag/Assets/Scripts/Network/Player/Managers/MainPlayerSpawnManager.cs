using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainPlayerSpawnManager : MonoBehaviour
{
    private string id = "null";
    private DataWrap curPlayer;
    public GameObject player;
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

    public void SpawnPlayer(TestModel md) {
        GameObject obj = Instantiate(player, Vector3.zero, Quaternion.identity);
        curPlayer = new DataWrap { gameObj = obj, model = md };
        this.id = curPlayer.model.Id;
    }

    public bool PlayerExists() {
        return curPlayer != null;
    }

    /// <summary>
    /// completely delete the player
    /// </summary>
    public void DeleteCurPlayer() {
        Die();
        curPlayer = null;
    }

    public bool IsPlayer(TestModel model) {
        return curPlayer.model.Id.Equals(model.Id);
    }
    /// <summary>
    /// destroy the reference of the player on scene
    /// </summary>
    public void Die() {
        Destroy(curPlayer.gameObj);
    }
}
