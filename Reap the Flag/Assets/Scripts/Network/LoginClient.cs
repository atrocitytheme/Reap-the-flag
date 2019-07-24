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

    private void Awake()
    {
        client.Timeout = TimeSpan.FromSeconds(1);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
    }

    public void ConnecteToServer()
    {
        var isSuccess = Task.Run(() => Request()).Result;

        if (isSuccess)
        {
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
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("Name", extractFromInput(name)),
                new KeyValuePair<string, string>("Password", extractFromInput(password)),
                new KeyValuePair<string, string>("Id", extractFromInput(id))
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
