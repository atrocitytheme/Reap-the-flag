using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    Dictionary<string, DataWrap> players = new Dictionary<string, DataWrap>();
    public GameObject player;

    public GameObject RetrievePlayer(string id) {
        if (players.ContainsKey(id)) return players[id].gameObj;

        return null;
    }

    private void FixedUpdate()
    {
    }
    public virtual void SpawnPlayer(TestModel md)
    {
        GameObject obj = Instantiate(player, new Vector3(0, 5, 0), Quaternion.identity);
        players.Add(md.Id, new DataWrap { gameObj = obj, model = md});
    }

    public void DeletePlayer(string id) {
        if (players.ContainsKey(id)) {
            Destroy(players[id].gameObj);
            players.Remove(id);
        }
    }
}
