using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour {

	// settings for the GUI
	public GUIStyle TitleStyle;
	public GUIStyle SubStyle;

	// Variables for the cheat code
	private int sequenceIndex;
	private KeyCode[] sequence = new KeyCode[] {
		KeyCode.J,
		KeyCode.O,
		KeyCode.E,
		KeyCode.L,
		KeyCode.S,
		KeyCode.P,
		KeyCode.E,
		KeyCode.A,
		KeyCode.N,
		KeyCode.U,
		KeyCode.T,
		KeyCode.S
	};

	void OnGUI () 
	{
		// display the title screend
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-100, 100, 50), "InnKeeper", TitleStyle);
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2+100, 100, 50), "Press the SPACEBAR to begin.", SubStyle);
	}

	void Update ()
	{
		// Start the game
		if( Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine (performFade3());
		}

		// Recognize the cheat JOELSPEANUTS
		if (Input.GetKeyDown (sequence [sequenceIndex])) 
		{
			if (++sequenceIndex == sequence.Length) 
			{
				sequenceIndex = 0;
				Inn.day = 6;						// Set day to 6, so when game starts its 7th day (last day)
				Inn.playerScore_net = 2000000000;	// Set the inn's total profit to 2 billion (to guaruntee a win)
				StartCoroutine (performFade3 ());
			}
		}
	}

	IEnumerator performFade3 ()
	{
		// fade out the scene
		float fadeTime = GameObject.Find("ScreenFader").GetComponent<SceneFade>().BeginFade(1);    
		yield return new WaitForSeconds(fadeTime);
		if (Inn.day == 0)
			SceneManager.LoadScene("Letter");
		else if (Inn.day == 6)
			SceneManager.LoadScene("main");

	}
}
