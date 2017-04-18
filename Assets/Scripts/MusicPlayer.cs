/**
 * This script controls the background music.
 * Getting the state of music upgrades from the store, the appropriate audio is played.
 * The audio is all stored to one game object with 12 audio sources, each matching a possible 
 * upgrade state.
 * Two songs exist, each with six possible upgrade states.
 * The progression of upgrade states adds an instrument to the layer of tracks following the progression:
 * Harpsichord -> Guitar -> Flute -> Lute -> Bass -> Piano (replaces Harpsichord)
 * 
 * all references to time will be replaced with an end-day correlation at a later day
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    int upgradeState; // upgrades state purchased from store
    bool songNum; // false is song 1, true is song 2
    int trackToPlay; // determined by upgradeState and songNum
    float time; // TBR remove
    bool fadingIn, fadingOut;// used determine if the fading in or fading out of a song should occur
    float volumeIn, volumeOut;// used to alter the volume during fading
    int inTrack, outTrack;// used to keep track of the song which is fading in or out
    List<AudioSource> songs = new List<AudioSource>();
    

    // Initializes variables and starts first song
    void Start ()
    {
        List<AudioSource> songs = new List<AudioSource>();
        
        GetComponents(songs);

        upgradeState = 0;

        songNum = false; // song 1

        trackToPlay = inTrack = outTrack = 0;

        songs[trackToPlay].Play();

        time = 0;

        fadingIn = fadingOut = false;

        volumeIn = 0f;
        volumeOut = 100f;

	}

    // Update is called once per frame
    // Determines when day ends, and enables fade transitions in conjunction with Music() method.
    void Update()
    {
        // TBR end of day sequence
        time = time + Time.deltaTime;
        if (time >= 15f)
        {
            time = 0;
            Music();
        }
        
        

        //fading in a new song
        if(fadingIn == true)
        {
            if (volumeIn < 100f)
            {
                volumeIn = volumeIn + 1f;
                GetComponents(songs);
                songs[inTrack].volume = volumeIn / 100f;
            }      
            else
            {
                volumeIn = 0f;
                fadingIn = false;
            }

        }
        
            //fading out an old song
            if(fadingOut == true)
            {

                GetComponents(songs);
                if (volumeOut > 0f)
                {
                    volumeOut = volumeOut - 1f;
                    songs[outTrack].volume = volumeOut / 100f;
                }
                else
                {
                    volumeOut = 100f;
                    songs[outTrack].Stop();
                    fadingOut = false;
                }
            }
     
        
    }

    //Forces transition from one song to another, checking the upgrade state.
    void Music()
    {
        GetComponents(songs);

        //fade out current track
        outTrack = trackToPlay;
        fadingOut = true;

        //update variables to determine next track to play
        songNum = !songNum;
        upgradeState = upgradeState + 1; // TBR a call to the store music upgrade variable
        if(songNum == false) // song 1
        {
            trackToPlay = upgradeState;
        }
        else// songNum == true, song 2
        {
            trackToPlay = upgradeState + 6;
        }

        //fade in new track
        inTrack = trackToPlay;
        songs[inTrack].volume = 0f;
        songs[inTrack].Play();
        fadingIn = true;
        
     }
   
}
