using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
	public GUIStyle HUDStyle;	// settings for the GUI

	void OnGUI () 
	{
		GUI.Label (new Rect (150, 72, 100, 100), "Your Money: " + Inn.playerScore_now.ToString(), HUDStyle);
		//GUI.Label (new Rect (130, 80, 100, 100), "Rival Money: " + Inn.opponentScore.ToString(), HUDStyle);
		GUI.Label (new Rect (150, 100, 100, 100), "Time Left: " + Inn.hour.ToString() + ":" + Inn.minute.ToString().PadLeft(2,'0'), HUDStyle);
	}
		
}