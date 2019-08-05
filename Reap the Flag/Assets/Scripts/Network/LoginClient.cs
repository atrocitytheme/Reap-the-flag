using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginClient : MonoBehaviour
{
    public LoginPanelOperator panel;
    public string url;
    public InputField id;
    public InputField newName;
    public InputField password;
    HttpClient client = new HttpClient();
    private static readonly Queue<Action> queue = new Queue<Action>();

    private void Awake()
    {
        client.Timeout = TimeSpan.FromSeconds(5);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
    }

    private void Update()
    {
        HandleTask();
    }

    void HandleTask() {
        while (queue.Count > 0) {
            Action curTask = null;
            lock (queue) {
                if (queue.Count > 0) {
                    curTask = queue.Dequeue();
                }
            }
            curTask();
        }
    }

    public void ConnecteToServer()
    {
        Task.Run(() => Request());
    }
    private string extractFromInput(InputField field) {
        return field.text;
    }

    private async Task Request() {
        string input_name = extractFromInput(newName);
        string input_password = extractFromInput(password);
        string input_id = extractFromInput(id);
        string phrase = "not initialized";
        try
        {
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("Name", input_name),
                new KeyValuePair<string, string>("Password", input_password),
                new KeyValuePair<string, string>("Id", input_id)
            });
            var msg = await client.PostAsync(url, content);
            phrase = await msg.Content.ReadAsStringAsync();
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            Debug.Log("phrase: " + phrase);
            QueueOnMainThread(()=> {
                panel.DisplayInfo(phrase);
            });
            return;
        }

        catch (InvalidOperationException)
        {
            QueueOnMainThread(() => {
                panel.DisplayInfo(phrase);
            });
            return;
        }

        catch (TaskCanceledException)
        {
            QueueOnMainThread(() => {
                panel.DisplayInfo(phrase);
            });
            return;
        }

        QueueOnMainThread(() => {
            Debug.Log("room id received: ..." + Int32.Parse(phrase));
            PlayerPrefs.SetInt("roomId", Int32.Parse(phrase));
            PlayerPrefs.SetString("newName", input_name);
            PlayerPrefs.SetString("password", input_password);
            PlayerPrefs.SetString("id", input_id);
            panel.DisplayInfo("loading scene...");
            try
            {
                
                SceneManager.LoadScene("GameScene_1");
            }
            catch (Exception e) {
                panel.DisplayInfo("error occurs!");
            }
        });
    }

    public void QueueOnMainThread(Action act) {
        lock (queue) {
            queue.Enqueue(act);
        }
    }
}
