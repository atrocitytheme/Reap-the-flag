using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBoard : MonoBehaviour
{
    public Text textField;
    public void InjectValue(string value) {
        textField.text = value;
    }
}
