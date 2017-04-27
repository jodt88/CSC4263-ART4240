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
	public string letterContent;
	public string letterSubContent = "Press the DOWN/UP ARROW to view Next/Previous page.";
	public List<string> openingLetterContent = new List<string>(); 
	public List<string> endingLetterContent = new List<string>();

	void Awake(){
		createLetterPages ();
	}

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
				endingLetterContent[0], LetterStyle);
			GUI.Label (new Rect (Screen.width / 2-50, Screen.height / 2+200, 100, 50), "Press SPACEBAR to continue.", SubStyle);
		}
		else if (Inn.day == 8 && Inn.opponentScore < Inn.playerScore_net)
		{
			GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 25, 100, 50),
				"Dear New Guy," +
				"\n\n" +
				"   Conglaturation! Who would've thought a nameless twit like\n" +
				"you would actually beat me at my own game! I mean seriously?!\n" +
				"How?!?! Well...as per our deal, I'll leave town, try to carve\n" +
				"out myfortune elsewhere. I hear Borrowind is lovely this time\n" +
				"of year. Well, anyway, best of luck to you with this po-dunk\n" +
				"town. You'll need it." +
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
				"counselling to overcome my extreme narcissism? Our agreement doesn't\n" +
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
		if (Input.GetKeyDown (KeyCode.DownArrow) && letterPage < 5) {
			letterPage++;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) && letterPage>1){
			letterPage--;
		}

		switch(letterPage){
		case 1:
			letterContent = openingLetterContent[0];
			break;
		case 2:
			letterContent = openingLetterContent[1];
			break;
		case 3:
			letterContent = openingLetterContent [2];
			break;
		case 4:
			letterContent = openingLetterContent[3];
			letterSubContent = "Press the DOWN/UP ARROW to view Next/Previous page.";
			//letterSubContent = "Press the SPACEBAR to continue.";
			break;
		case 5:
			letterContent = openingLetterContent [4];
			letterSubContent = "Press the SPACEBAR to continue.";
			break;

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

	void createLetterPages(){
		openingLetterContent.Add ("Dear New Guy," +
			"\n\n" +
			"   Hi! It's me, your rival. You know, from the VERY successful inn\n" +
			"a few paces away. I doubt you'll ever reach my caliber of success.\n" +
			"But I'll give you the benefit of the doubt. Let's make our new\n" +
			"business rivalry a bit more interesting. Let's say, in a week's time,\n" +
			"if my inn makes more money than yours, you make like a corrupt jarl\n" +
			"and leave town. In the very unlikely chance that your inn makes more\n" +
			"money than mine, I shut down. I'll even throw in an old copy of The\n" +
			"Innkeeper's Guide, to keep you from making a complete fool of yourself.\n" +
			"Best of luck to you with your squalor." +
			"\n\n" +
			"Yours Truly,\n" +
			"The best innkeeper in town\n" +
			"from the Jagged Dragon Bar & Inn\n");

		openingLetterContent.Add ("THE INNKEEPERS GUIDE\n\n" +
			"Hello there! So you decided to buy an inn. So what comes next?\n" +
			"With this short-but-sweet guide to innkeeping, you're sure to\n" +
			"become a success in no time. So let's get down to business...\n" +
			"\n" +
			"THE FRONT COUNTER:\n" +
			"Patrons who enter your inn will likely want one of two things,\n" +
			"a bed to rest their weary head, or a table with which they can\n" +
			"fill their bellies with mead; you must be behind the counter\n" +
			"to tend to them, so be sure to man that station regularly.\n" +
			"If you are not there for a while, patrons may leave angry.");

		openingLetterContent.Add ("CLEANLINESS:\n" +
			"Once a patron sleeps in a bed or drinks at a spot at a table,\n" +
			"they are sure to leave a mess when they leave. It is up to\n" +
			"you to make sure that the area is clean for others who come\n" +
			"into your inn. Just make your way to the mess, and try to clean\n" +
			"it as quickly as possible so you can return to the front counter.\n" +
			"\n" +
			"HAPPINESS:\n" +
			"It's important to keep your patron's happy, because it is ultimately\n" +
			"their business that pays the jarl's taxes. A happy patron will pay\n" +
			"you on their way out. A dissatisfied patron will leave in favor of\n" +
			"someone else, bringing you ever so closer to debt.");

		openingLetterContent.Add ("RESOURCES:\n" +
			"At some point during your business endeavors, you're going to\n" +
			"need to buy more items. This could be in the form of more beds,\n" +
			"tables, or minstrels to provide entertainment to your inn (which\n" +
			"makes customers happier, leading to higher tips). The inn is too\n" +
			"busy during the day to shop around for such things. Once a\n" +
			"business day has concluded, you can purchase items from the store.\n" +
			"Be wise with how you spend your money though, because those funds\n" +
			"come straight from the inn's profit.");

		openingLetterContent.Add ("\n" +
			"FINANCES AND RIVALRY:\n" +
			"Rivalry comes with the territory of owning an inn. It's\n" +
			"important to remain cordial with your rival, but don't let them\n" +
			"overshadow your success. At the end of the day, you can review\n" +
			"your finances as well as those of your rival to determine how you\n" +
			"are doing competitively.\n" +
			"\n" +
			"So that about sums it up. Now you're ready to experience the joys\n" +
			"of running your own inn. Good luck to you, and may the Divines\n" +
			"smile on you, friend!\n\n" +
			"-Anonymous");
	}
}
