using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameClock : MonoBehaviour {
	public static double lastChange = 0;

	void Update () 
	{
		if (Inn.minute == 0 && Inn.hour == 0) 
		{
			Inn.day++;
			SceneManager.LoadScene ("End Level");
		} 
		else 
		{
			if (Time.time - lastChange > 1.0) 
			{
				if (Inn.minute == 0) 
				{
					Inn.minute = 59;
					Inn.hour--;
				}
				Inn.minute--;
				lastChange = Time.time;
			}
		}
	}
}
