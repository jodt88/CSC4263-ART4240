using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public static double lastChange = 0;
    public static bool musicFadeOutTrigger; //Utilized by the MusicPlayer Script

    void Start()
    {
        // set hour and minute if scene is the main scene (currently approx. real-time = 5 minutes)
        if (SceneManager.GetActiveScene().name == "main")
        {
            Inn.hour = 0;
            Inn.minute = 60;
							// also increment the day
			Inn.playerScore_now = 0;	// also reset day's profit to 0
        }
        // set hour and minute if scene is the end of day recap scene (currently approx. real-time = 5 seconds)
        else if (SceneManager.GetActiveScene().name == "End of Day Recap")
        {
			Inn.day++;	
            Inn.hour = 0;
            Inn.minute = 10;
        }

        musicFadeOutTrigger = false;
    } 

    void Update()
    {
        // if clock reaches 0 hours and 0 minutes...
        if (Inn.minute == 0 && Inn.hour == 0)
        {
            StartCoroutine(performFade());      // ...fade into the next scene
        }
        // if clock has time remaining...
        else
        {

            if (Time.time - lastChange > 1.0)   // sets time interval to 1 second (pt. 1)
            {
                // if no more minutes remain...
                if (Inn.minute == 0)
                {
                    Inn.minute = 59;            // ...reset seconds to 59
                    Inn.hour--;                 // ...decrement the hour
                }
                Inn.minute--;                   // decrement the minute
                lastChange = Time.time;         // sets time interval to 1 second (pt. 2)
            }
        }
    }

    IEnumerator performFade()
    {
        // fade out the scene
        float fadeTime = GameObject.Find("ClockAndFade").GetComponent<SceneFade>().BeginFade(1);    
        yield return new WaitForSeconds(fadeTime);

        // check what the current scene is, and load the appropriate scene
        if (SceneManager.GetActiveScene().name == "main")
        {
            SceneManager.LoadScene("End of Day Recap");
            musicFadeOutTrigger = true;
        }
        else if (SceneManager.GetActiveScene().name == "End of Day Recap")
            SceneManager.LoadScene("Store");
		else if (SceneManager.GetActiveScene().name == "End of Day Recap" && Inn.day == 7)
			SceneManager.LoadScene("Letter");
    }
}
