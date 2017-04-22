using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class EOD_display : MonoBehaviour
{
	public GUIStyle HUDStyle;	// settings for the GUI

	void OnGUI () 
	{
		// display a recap of the day's information
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-160, 100, 50), "DAY " + Inn.day + " RECAP", HUDStyle);
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-35, 100, 50), "Day's Profit: " + Inn.playerScore_now.ToString(), HUDStyle);
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2+30, 100, 50), "Total Profit: " + Inn.playerScore_net.ToString(), HUDStyle);
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2+95, 100, 50), "Rival's Profit: " + Inn.opponentScore.ToString(), HUDStyle);
	}

	void Start ()
	{
		// add the day's profit to the inn's total profit
		Inn.playerScore_net += Inn.playerScore_now;
	}
}