using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// rotate the skybox automatically
/// </summary>
public class RotateSkybox : MonoBehaviour
{
    float rotation = 106;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
        rotation += Time.deltaTime * 5;

        if (rotation >= 360) {
            rotation = 0;
        }
    }
}
