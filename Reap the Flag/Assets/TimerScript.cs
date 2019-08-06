using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    int timer = 300;
    public Text counter;
    void Start()
    {
        StartCoroutine(StartTimeout());
    }

    public IEnumerator StartTimeout() {
        while (timer > 0) {
            yield return new WaitForSeconds(1f);
            timer--;
            counter.text = "" + timer;
        }
    }

    public void SetTimer(int time) {
        Debug.Log("current time: " + time);
        timer = time;
    }
}
