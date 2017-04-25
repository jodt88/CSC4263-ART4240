using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class rivalLetter : MonoBehaviour {

	public GUIStyle LetterStyle;	// settings for the GUI
	public GUIStyle SubStyle;	// settings for the GUI

    public static bool winGame = false;


	void OnGUI()
	{
		if (Inn.day == 1) 
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Hi! It's me, your rival. You know, from the VERY successful inn\n" +
				"a few paces away. I doubt you'll ever reach my caliber of success.\n" +
				"But I'll give you the benefit of the doubt. Let's make our new\n" +
				"business rivalry a bit more interesting. Let's say, in a week's time,\n" +
				"if my inn makes more money than yours, you make like a corrupt jarl\n" +
				"and leave town. In the very unlikely chance that your inn makes more\n" +
				"money than mine, I shut down. Best of luck to you with your squalor." +
				"\n\n" +
				"Yours Truly,\n" +
				"The best innkeeper in town\n" +
				"from the Jagged Dragon Bar & Inn", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 8 && Inn.opponentScore > Inn.playerScore_net)
		{
            winGame = true;
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Ha ha ha ha ha ha ha! I knew you couldn't cut it! Let's face it,\n" +
				"you had a good run, but in the end, you just couldn't hack it against\n" +
				"me. I have the most successful inn in all of the nine realms, yet you\n" +
				"seriously thought you could beat me. I don't know where you're gonna go\n" +
				"or do from here, and I don't rightly care. Happy trails, and don't let\n" +
				"the door hit your rags on the way out!" +
				"\n\n" +
				"Clearly your better,\n" +
				"The best innkeeper in town\n" +
				"from the Jagged Dragon Bar & Inn\n\n" +
				"P.S. Ha ha ha ha ha!", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 8 && Inn.opponentScore < Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Dang it! Who would've thought a nameless twit like you would\n" +
				"actually beat me at my own game! I mean seriously?! How?!?!\n" +
				"Well...as per our deal, I'll leave town, try to carve out my\n" +
				"fortune elsewhere. I hear Borrowind is lovely this time of year.\n" +
				"Well, anyway, best of luck to you with this po-dunk town. You'll\n" +
				"need it." +
				"\n\n" +
				"Apparently your inferior,\n" +
				"The worst innkeeper in town\n" +
				"formerly from the Jagged Dragon Bar & Inn\n\n", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 8 && Inn.opponentScore == Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Well this is embarrassing. Not really sure how this happened\n" +
				"really. You made the same amount of money as me. So what do we do?\n" +
				"Does this mean I actually have to treat you kindly? Do I seek\n" +
				"counseling to overcome my extreme narcissism? Our agreement doesn't\n" +
				"say anything about us being equals. I really didn't think this\n" +
				"through, did I? I guess this means we both go our seperate ways and\n" +
				"live happily ever after. As much as it pains me to say it, I guess\n" +
				"we're equals." +
				"\n\n" +
				"Yours truly,\n" +
				"An innkeeper in town\n" +
				"from the Jagged Dragon Bar & Inn\n\n", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
	}

	void Update ()
	{
		// Start the game
		if( Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine (performFade3());
		}
	}

	IEnumerator performFade3 ()
	{
		// fade out the scene
		float fadeTime = GameObject.Find("ScreenFader").GetComponent<SceneFade>().BeginFade(1);    
		yield return new WaitForSeconds(fadeTime);
		if (Inn.day == 1)
			SceneManager.LoadScene ("main");
		else if (Inn.day == 8) 
		{
			resetValues ();
			SceneManager.LoadScene ("Title");
		}
		
	}

	void resetValues() 
	{
		Inn.day = 1;
		Inn.opponentScore = Inn.playerScore_net = Inn.playerScore_now = 0;
	}
}
