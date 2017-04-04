using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
	public GUIStyle HUDStyle;

	void OnGUI () 
	{
		GUI.Label (new Rect (90, 90, 100, 100), "Money: " + Inn.playerScore.ToString(), HUDStyle);
	}
}