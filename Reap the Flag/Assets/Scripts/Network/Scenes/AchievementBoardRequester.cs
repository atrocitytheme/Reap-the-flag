using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementBoardRequester : MonoBehaviour
{
    public KetFrameClient client;
    bool startRequest = false;
    public GameObject displayBoard;
    int timeout = 0;
    private string id;
    private string newName;
    private string password;
    private Dictionary<string, int> data;

    private void Start()
    {
        id = PlayerPrefs.GetString("id");
        password = PlayerPrefs.GetString("password");
        newName = PlayerPrefs.GetString("newName");
        data = new Dictionary<string, int>();
        timeout = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            displayBoard.SetActive(true);
            startRequest = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            displayBoard.SetActive(false);
            startRequest = false;
        }

        if (Input.GetKey(KeyCode.Tab)) {
            timeout -= 1;
        }

        if (startRequest && timeout <= 0) {
            
            QueryData();
        }
    }

    public void AddData(string id, int curInput) {
        if (data.ContainsKey(id)) data.Remove(id);
        data.Add(id, curInput);
    }

    private void QueryData() {
        client.AskForKeyFrame(new TestModel { CommandType = 7, Name = newName, Id = id, Password = password });
        timeout = 100;
    }

    public void InjectValue() {
        string curString = "";

        foreach (KeyValuePair<string, int> entry in data) {
            curString += (entry.Key + ":   " + entry.Value + "\n");
        }
        Debug.Log(curString);
        displayBoard.GetComponent<DataBoard>().InjectValue(curString);
        data.Clear();
    }
}
