using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    int songNum;
    float time;
    List<AudioSource> songs = new List<AudioSource>();
    

    // Use this for initialization
    void Start ()
    {
        List<AudioSource> songs = new List<AudioSource>();
        
        GetComponents(songs);
        
        songNum = 0;

        songs[songNum].Play();
        
        time = 0;

	}

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if (time >= 30f)
        {
            time = 0;
            Music();
        }
    }

    void Music()
    {
        GetComponents(songs);
        
        songs[songNum].Stop();
        if (songNum == 1) //number of songs + 1
            songNum = 0;
        else
            songNum++;
        songs[songNum].Play();
     }
    
}
