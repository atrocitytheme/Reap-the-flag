using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFade : MonoBehaviour
{

    public Texture2D fadeImage;
    public float fadeRate;

    int depth = -1000;
    float alpha = 1.0f; // the transparancy of the graph
    int fadeDir = -1;
    private void OnGUI()
    {
        alpha += fadeDir * fadeRate * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = depth;
    }

    public float Fade(int dir) {
        fadeDir = dir;
        return 1 / fadeRate;
    }
    private void Start()
    {
        Fade(1);
    }
}
