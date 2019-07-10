using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A dummy client without logging in
/// This client won't connect to the server but merely check
/// the surrounding of a scene
/// </summary>
public class TestClient : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public void ConnecteToServer()
    {
        SceneManager.LoadScene("GameScene_1");
    }
}
