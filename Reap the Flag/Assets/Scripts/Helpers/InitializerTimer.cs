using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializerTimer : MonoBehaviour
{
    public Text DisplayCounter;
    float timer = 1.0f;
    // Update is called once per frame

    public bool IsValid {
        get {
            return timer < 0;
        }
    }
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime * 2;
        }
    }
}
