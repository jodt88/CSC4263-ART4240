using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class rivalLetter : MonoBehaviour {

	public GUIStyle LetterStyle;	// settings for the GUI
	public GUIStyle SubStyle;	// settings for the GUI


	void OnGUI()
	{
		if (Inn.day == 0) 
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
		else if (Inn.day == 7 && Inn.opponentScore > Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Ha ha ha ha ha ha ha! I knew you couldn't cut it! Let's face it, you\n" +
				"had a good run, but in the end, just couldn't hack it against me. I have\n" +
				"the most successful inn in all of the nine realms, yet you seriously\n" +
				"thought you could challenge me. I don't know where you're gonna go or do\n" +
				"from here, and I don't rightly care. Happy trails, and don't let the door\n" +
				"hit your rags on the way out!" +
				"\n\n" +
				"Clearly your better,\n" +
				"The best innkeeper in town\n" +
				"from the Jagged Dragon Bar & Inn\n\n" +
				"P.S. Ha ha ha ha ha!", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 7 && Inn.opponentScore < Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Dang it! Who would've thought a nameless twit like you would actually\n" +
				"beat me at my own game! I mean seriously?! How?!?! Well...as per our deal,\n" +
				"I'll leave town, try to carve out my fortune elsewhere. I hear Borrowind\n" +
				"is lovely this time of year. Well, anyway, best of luck to you with the\n" +
				"po-dunk town. You'll need it." +
				"\n\n" +
				"Apparently your inferior,\n" +
				"The worst innkeeper in town\n" +
				"formerly from the Jagged Dragon Bar & Inn\n\n", LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 7 && Inn.opponentScore == Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Well this is embarrassing. Not really sure how this happened really.\n" +
				"You made the same amount of money as me. So what do we do?\n" +
				"Does this mean I actually have to treat you kindly? Do I seek counseling\n" +
				"to overcome my extreme narcissism? Our agreement doesn't say anything\n" +
				"about us being equals. I really didn't think this through, did I? I\n" +
				"guess this means we both go our seperate ways and live happily ever\n" +
				"after. As much as it pains me to say it, I guess we're equals." +
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
		if (Inn.day == 0)
			SceneManager.LoadScene("main");
		else if (Inn.day == 7)
			SceneManager.LoadScene("Title");
		
	}
}
