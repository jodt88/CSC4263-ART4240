using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class rivalLetter : MonoBehaviour {

	public GUIStyle LetterStyle;	// settings for the GUI
	public GUIStyle SubStyle;	// settings for the GUI

    public static bool winGame = false;

	public int letterPage = 1;
	public string letterSubContent = "Press the DOWN ARROW to go to next page.";
	public string letterContent = 
		"Dear New Guy," +
		"\n\n" +
		"   Hi! It's me, your rival. You know, from the VERY successful inn\n" +
		"a few paces away. I doubt you'll ever reach my caliber of success.\n" +
		"But I'll give you the benefit of the doubt. Let's make our new\n" +
		"business rivalry a bit more interesting. Let's say, in a week's time,\n" +
		"if my inn makes more money than yours, you make like a corrupt jarl\n" +
		"and leave town. In the very unlikely chance that your inn makes more\n" +
		"money than mine, I shut down. I'll even through in an old copy of The\n" +
		"Innkeeper's Guide, to keep you from making a complete fool of yourself.\n" +
		"Best of luck to you with your squalor." +
		"\n\n" +
		"Yours Truly,\n" +
		"The best innkeeper in town\n" +
		"from the Jagged Dragon Bar & Inn\n";


	void OnGUI()
	{
		if (Inn.day == 1) 
		{	
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50), letterContent, LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), letterSubContent, SubStyle);
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
		if (Input.GetKeyDown(KeyCode.DownArrow) && letterPage == 1)
		{
			letterPage++;
			letterContent = 
				"THE INNKEEPERS GUIDE\n\n" +
				"Hello there! So you decided to buy an inn. So what comes next?\n" +
				"With this short-but-sweet guide to innkeeping, you're sure to\n" +
				"become a success in no time. So let's get down to business...\n" +
				"\n" +
				"THE FRONT COUNTER:\n" +
				"Patrons who enter your inn will likely want one of two things,\n" +
				"a bed to rest their weary head, or a table with which they can\n" +
				"fill their bellies with mead; you must be behind the counter\n" +
				"to tend to them, so be sure to man that station regularly.\n" +
				"If you are not there for a while, patrons may leave angry.";
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && letterPage == 2)
		{
			letterPage++;
			letterContent =
				"CLEANLINESS:\n" +
				"they are sure to leave a mess when they leave. It is up to\n" +
				"you to make sure that the area is clean for others who come\n" +
				"into your inn. Just make your way to the mess, and try to clean\n" +
				"\n" +
				"HAPPINESS:\n" +
				"It's important to keep your patron's happy, because it is ultimately\n" +
				"their business that pays the jarl's taxes. A happy patron will pay\n" +
				"you on their way out. A dissatisfied patron will leave in favor of\n" +
				"someone else, bringing you ever so closer to debt.";
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && letterPage == 3)
		{
			letterPage++;
			letterContent =
				"RESOURCES:\n" +
				"At some point during your business endeavors, you're going to\n" +
				"need to buy more items. This could be in the form of more beds,\n" +
				"tables, or minstrels to provide entertainment to your inn. The\n" +
				"inn is too busy during the day to shop around for such things.\n" +
				"Once a business day has concluded, you can purchase items from\n" +
				"the store. Be wise with how you spend your money though,\n" +
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && letterPage == 4)
		{ 
			letterContent =
				"\n" +
				"FINANCES AND RIVALRY:\n" +
				"Rivalry comes with the territory of owning an inn. It's\n" +
				"important to remain cordial with your rival, but don't let them\n" +
				"your finances as well as those of your rival to determine how you\n" +
				"are doing competitively.\n" +
				"\n" +
				"So that about sums it up. Now you're ready to experience the joys\n" +
				"of running your own inn. Good luck to you, and may the Divines\n" +
				"smile on you!\n\n" +
				"-Anonymous";
			letterSubContent = "Press the SPACEBAR to continue.";
		}

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
