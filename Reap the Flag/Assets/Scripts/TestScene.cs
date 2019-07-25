using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void Test() {
        SceneManager.LoadScene("GameScene_1");
    }
}
