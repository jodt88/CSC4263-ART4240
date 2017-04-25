using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameMusic : MonoBehaviour
{
    List<AudioSource> songs = new List<AudioSource>();
    float volumeOut = 100f;

    // Use this for initialization
    void Start()
    {
        List<AudioSource> songs = new List<AudioSource>();

        GetComponents(songs);
        if(rivalLetter.winGame == true)
            songs[0].Play();
    }

    
}
