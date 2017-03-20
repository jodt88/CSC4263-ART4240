using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public int songNum;
    float time;
    List<AudioSource> songs = new List<AudioSource>();
    

    // Use this for initialization
    void Start ()
    {
        //List<AudioSource> songs = new List<AudioSource>();
        
        GetComponents(songs);
        
        songNum = 0;

        songs[songNum].Play();
        
        time = 0;

	}

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f)
        {
            Music();
        }
    }

    void Music()
    {
        GetComponents(songs);
        

        songs[songNum].Stop();
        //if (songNum == 1)
        //    songNum = -1; // will increment to 0 at songNum++
        //songNum++;
        //songs[songNum].Play();
     }
    
}
