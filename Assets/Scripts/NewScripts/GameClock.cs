﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameClock : MonoBehaviour
{
	public static double lastChange = 0;
	public static bool musicFadeOutTrigger; //Utilized by the MusicPlayer Script

	void Awake()
    {
		SetDayTimer (3,00);
	}

    void Start()
	{
		// set hour and minute if scene is the main scene (currently approx. real-time = 5 minutes)
		if (SceneManager.GetActiveScene ().name == "main") {

			SetDayTimer (3,00);
            Inn.playerScore_now = 0;
        }
		// set hour and minute if scene is the end of day recap scene (currently approx. real-time = 5 seconds)
		else if (SceneManager.GetActiveScene().name == "End of Day Recap")
		{
			SetTransitionDayTimer (5);
		}

		musicFadeOutTrigger = false;
	} 

	void SetDayTimer(int hours,int minutes){
		Inn.hour = hours;
		Inn.minute = minutes;
        

    }

	void SetTransitionDayTimer(int seconds){
		Inn.day++;
		Inn.hour = 0;
		Inn.minute = seconds;
        
    }

	void Update()
	{
		// if clock reaches 0 hours and 0 minutes...
		if (SceneManager.GetActiveScene ().name == "main"||SceneManager.GetActiveScene ().name == "End of Day Recap") {
			if (Inn.minute == 0 && Inn.hour == 0) {
				StartCoroutine (performFade ());      // ...fade into the next scene
			}
			// if clock has time remaining...
			else {

				if (Time.time - lastChange > 1.0) {   // sets time interval to 1 second (pt. 1)
					// if no more minutes remain...
					if (Inn.minute == 0) {
						Inn.minute = 59;            // ...reset seconds to 59
						Inn.hour--;                 // ...decrement the hour
					}
					Inn.minute--;                   // decrement the minute
					lastChange = Time.time;         // sets time interval to 1 second (pt. 2)
				}
			}
		}
	}

	IEnumerator performFade()
	{
		// fade out the scene
		float fadeTime = GameObject.Find ("ClockAndFade").GetComponent<SceneFade> ().BeginFade (1);    
		yield return new WaitForSeconds (fadeTime);

		// check what the current scene is, and load the appropriate scene
		if (SceneManager.GetActiveScene ().name == "main") {
			SceneManager.LoadScene ("End of Day Recap");
			musicFadeOutTrigger = true;
		} 
		else if (SceneManager.GetActiveScene ().name == "End of Day Recap") 
		{
			if (Inn.day == 8)
				SceneManager.LoadScene ("Letter");
			else
				SceneManager.LoadScene ("Store");
		}
	}
}
