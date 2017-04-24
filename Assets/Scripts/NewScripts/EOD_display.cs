using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class EOD_display : MonoBehaviour
{
	// settings for the GUI
	public GUIStyle TitleStyle;
	public GUIStyle SubStyle;

	void OnGUI () 
	{
		// display a recap of the day's information
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-125, 100, 50), "Day " + Inn.day + " Recap", TitleStyle);
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2+50, 100, 50), 
			"Day's Profit: " + Inn.playerScore_now.ToString() + "\n\n" +
			"Total Profit: " + Inn.playerScore_net.ToString() + "\n\n" +
			"Rival's Profit: " + Inn.opponentScore.ToString(),  SubStyle);
	}

	void Start ()
	{
		// add the day's profit to the inn's total profit
		Inn.playerScore_net += Inn.playerScore_now;
	}
}