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
    public InputField name;
    public InputField password;
    HttpClient client = new HttpClient();
    private string input_name;
    private string input_password;
    private string input_id;

    public string Name {
        get
        {
            return input_name;
        }
    }

    public string Id {
        get {
            return input_id; 
        }
    }

    public string Password {
        get {
            return input_password;
        }
    }
    private void Awake()
    {
        client.Timeout = TimeSpan.FromSeconds(1);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        DontDestroyOnLoad(this.gameObject);
    }

    public void ConnecteToServer()
    {
        var isSuccess = Task.Run(() => Request()).Result;

        if (isSuccess)
        {
            PlayerPrefs.SetString("name", input_name);
            PlayerPrefs.SetString("password", input_password);
            PlayerPrefs.SetString("id", input_id);

            SceneManager.LoadScene("GameScene_1");
            return;
        }

        panel.DisplayInfo(isSuccess);
        
    }
    private string extractFromInput(InputField field) {
        return field.text;
    }

    private async Task<bool> Request() {
        try
        {
            input_name = extractFromInput(name);
            input_password = extractFromInput(password);
            input_id = extractFromInput(id);
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("Name", input_name),
                new KeyValuePair<string, string>("Password", input_password),
                new KeyValuePair<string, string>("Id", input_id)
            });
            var msg = await client.PostAsync(url, content);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            return false;
        }

        catch (InvalidOperationException)
        {
            return false;
        }

        catch (TaskCanceledException)
        {
            return false;
        }

        return true;
    }
}
