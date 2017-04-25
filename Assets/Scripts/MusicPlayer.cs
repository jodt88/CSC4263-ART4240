/**
 * This script controls the background music.
 * Getting the state of music upgrades from the store, the appropriate audio is played.
 * The progression of upgrade states adds an instrument to the layer of tracks following the progression:
 * Harpsichord -> Guitar -> Flute -> Lute -> Bass -> Piano (replaces Harpsichord)
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    int upgradeState; // upgrades state purchased from store
    bool fadingIn, fadingOut;// used determine if the fading in or fading out of a song should occur
    float volumeOut;// used to alter the volume during fading
    int outTrack;// used to keep track of the song which is fading in or out
    List<AudioSource> songs = new List<AudioSource>();
    

    // Initializes variables and starts first song
    void Start ()
    {
        List<AudioSource> songs = new List<AudioSource>();
        
        GetComponents(songs);

        upgradeState = StoreData.musicUpgrades;

        songs[upgradeState].Play();

        fadingOut = false;

        volumeOut = 100f;

	}

    // Update is called once per frame
    // Determines when day ends, and enables fade transitions in conjunction with Music() method.
    void Update()
    {
        if(GameClock.musicFadeOutTrigger == true)
        {
                GetComponents(songs);
                if (volumeOut > 0f)
                {
                    volumeOut = volumeOut - 1f;
                    songs[upgradeState].volume = volumeOut / 100f;
                }
                else
                {
                    volumeOut = 100f;
                    songs[upgradeState].Stop();
                    GameClock.musicFadeOutTrigger = false;
                }
            }
     
        
    }
   
}
