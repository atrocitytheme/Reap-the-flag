using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormModel : MonoBehaviour
{
    // Start is called before the first frame update
    private string name;
    private string id;
    private string password;

    public string Name {
        get
        {
            return name;
        }

        set {
            name = value;
        }
    }

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }
}
