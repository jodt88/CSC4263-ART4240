using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    List<AudioSource> songs = new List<AudioSource>();
    float volumeOut = 100f;

    // Use this for initialization
    void Start ()
    {
        List<AudioSource> songs = new List<AudioSource>();

        GetComponents(songs);

        songs[0].Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (mainMenu.pressedSpace == true)
        {

            GetComponents(songs);
            if (volumeOut > 0f)
            {
                volumeOut = volumeOut - 1f;
                songs[0].volume = volumeOut / 100f;
            }
            else
            {
                volumeOut = 100f;
                songs[0].Stop();
                mainMenu.pressedSpace = false;
            }
        }
    }
}
