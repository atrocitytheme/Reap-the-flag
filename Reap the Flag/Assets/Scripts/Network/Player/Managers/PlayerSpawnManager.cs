using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    Dictionary<string, DataWrap> players = new Dictionary<string, DataWrap>();
    public GameObject player;

    public DataWrap RetrievePlayer(TestModel model) {
        if (model == null) return null;
        return RetrievePlayer(model.Id);
    }

    public DataWrap RetrievePlayer(string id) {
        if (players.ContainsKey(id)) return players[id];

        return null;
    }

    public void SpawnPlayer(TestModel md)
    {
        if (!md.IsDead)
        {
            WorldPoint pt = md.Location.Location;
            WorldPoint rt = md.Rotation.Rotation;
            Vector3 spin = new Vector3((float)rt.X, (float)rt.Y, (float)rt.Z);
            GameObject obj = Instantiate(player, new Vector3((float)pt.X, (float)pt.Y, (float)pt.Z), Quaternion.Euler(spin));
            obj.GetComponent<OnlineIdentity>().RegisterIdentity(md);
            players.Add(md.Id, new DataWrap { gameObj = obj, model = md });
            obj.transform.position = new Vector3(100, 100, 100);
        }
    }

    public void DeletePlayer(string id) {
        if (players.ContainsKey(id)) {
            Destroy(players[id].gameObj);
            players.Remove(id);
        }
    }

    public bool Exists(TestModel model) {
        return players.ContainsKey(model.Id);
    }

    public void MovePlayer(TestModel model) {
        DataWrap wrapper = RetrievePlayer(model);
        if (wrapper == null) return;
    }
}
