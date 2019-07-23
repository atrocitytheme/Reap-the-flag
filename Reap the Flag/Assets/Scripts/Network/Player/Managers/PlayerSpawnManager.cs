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

    public void SpawnPlayer(TestModel md)
    {
        WorldPoint pt = md.Location.Location;
        WorldPoint rt = md.Rotation.Rotation;
        Vector3 spin = new Vector3((float)rt.X, (float)rt.Y, (float)rt.Z);
        GameObject obj = Instantiate(player, new Vector3((float)pt.X, (float)pt.Y, (float)pt.Z), Quaternion.Euler(spin));
        players.Add(md.Id, new DataWrap { gameObj = obj, model = md});
    }

    public void DeletePlayer(string id) {
        if (players.ContainsKey(id)) {
            Destroy(players[id].gameObj);
            players.Remove(id);
        }
    }
}
