using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelOperator : MonoBehaviour
{
    public Text infoBoard;
    static float timeout = 1.0f;  // the time out of the display info
    float timer = 0f;
    // Start is called before the first frame update
    public void DisplayInfo(bool isSuccess) {
        if (!isSuccess) {
            infoBoard.text = "Login failed!";
            timer = timeout;
        }
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
