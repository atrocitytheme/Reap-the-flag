using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelOperator : MonoBehaviour
{
    public Text infoBoard;
    static float timeout = 10.0f;  // the time out of the display info
    float timer = 0f;
    // Start is called before the first frame update
    public void DisplayInfo(string phrase) {
        infoBoard.text = phrase;
        timer = timeout;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= (Time.deltaTime);
        }

        else {
            timer = 0;
            infoBoard.text = "";
        }
    }
}
