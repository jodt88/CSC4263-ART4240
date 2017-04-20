using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneFade : MonoBehaviour
{

    public Texture2D fadeOutTexture;        // graphic to fade to
    public float fadeSpeed = 0.8f;          // fading speed

    private int drawDepth = -1000;          // order in the draw hierarchy
    private float alpha = 1.0f;             // alpha value
    private int fadeDir = -1;               // set to -1 to fade in; set to 1 to fade out

    void OnGUI()
    {
        // fade out/in the alpha value using a direction (fadeDir), speed (fadeSpeed), time in seconds (Time.deltaTime)
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // force (clamp) the number between 0 and 1 b/c GUI.color uses alpha values 0 and 1
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);                // set alpha
        GUI.depth = drawDepth;                                                              // set texture to render on top
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);       // draw texture to fit screen
    }

    // sets fadeDir to the direction parameter making the scene fade in/out
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);     // return the fadeSpeed var. so its easy to time scene change
    }

    // called when a level is loaded
    void OnLevelWasLoaded ()
    {
        BeginFade(-1);      // call fade in function
    }
}

