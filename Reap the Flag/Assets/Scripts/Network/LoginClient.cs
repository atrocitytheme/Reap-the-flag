using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Collections.Generic;

public class LoginClient : MonoBehaviour
{
    public LoginPanelOperator panel;
    public string url;
    HttpClient client = new HttpClient();
    public void ConnecteToServer()
    {
        var requestTask = Request();
        requestTask.Wait();
        bool isSuccess = requestTask.Result;

        if (isSuccess)
        {
            Debug.Log("Log in succeed!");
        }

        panel.DisplayInfo(isSuccess);
        
    }

    private async Task<bool> Request() {
        try
        {
            HttpResponseMessage msg = await client.GetAsync(url);
            msg.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            return false;
        }

        catch (InvalidOperationException) {
            return false;
        }

        return true;
    }
}
