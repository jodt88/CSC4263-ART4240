using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreData : MonoBehaviour
{
    int money;
    public static int bedUpgrades=0;
    public static int tableUpgrades=0;
    public static int musicUpgrades=0;
	public static int initialBeds = 2;
	public static int initialTables = 2;
    int maxBedUpgrades = 6;
    int maxTableUpgrades = 4;
    int maxMusicUpgrades = 5;
    int bedPrice = 200;
    int tablePrice = 100;
    int musicPrice = 300;
    public static bool pressedFour = false; //Utilized by StoreMusic script

    List<AudioSource> audio = new List<AudioSource>();

    public static bool musicFadeInTrigger; //Utilized by MusicPlayer Script

	public GUIStyle HUDStyle;	// settings for the GUI

	void OnGUI()
	{
		// display a recap of the day's information
		GUI.Label (new Rect (Screen.width/2-50, Screen.height/2, 100, 50),
			"1.) Beds \t\t Upgrades Left: " + (maxBedUpgrades-bedUpgrades) + "\tCost: " + bedPrice + "\n\n" +
			"2.) Tables \t\t Upgrades Left: " + (maxTableUpgrades-tableUpgrades) + "\tCost: " + tablePrice + "\n\n" +
			"3.) Minstrels \t Upgrades Left: " + (maxMusicUpgrades-musicUpgrades) + "\tCost: " + musicPrice + "\n\n\n" +
			"Total Money: " + money + "\n\n\n" +
			"Press 1, 2, or 3 on the keyboard to buy their matching upgrades.\n\n" +
			"Press 4 on the keyboard to continue to the next day.", HUDStyle);
	}

	// Use this for initialization
    void Start()
    {
        money = Inn.playerScore_net;
    } 

    void Update()
    {
        //Purchase bed
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (money >= bedPrice && bedUpgrades < maxBedUpgrades)
            {
                GetComponents(audio);
                money = money - bedPrice;
                bedUpgrades++;
                audio[1].Play(); //cha-ching
            }
            else
            {
                GetComponents(audio);
                audio[0].Play(); //error
            }
        }
        
        //Purchase table
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(money >= tablePrice && tableUpgrades < maxTableUpgrades)
            {
                GetComponents(audio);
                money = money - tablePrice;
                tableUpgrades++;
                audio[1].Play(); //cha-ching
            }
            else
            {
                GetComponents(audio);
                audio[0].Play(); //error
            }
        }
        
        //Purchase musician
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (money >= musicPrice && musicUpgrades < maxMusicUpgrades)
            {
                GetComponents(audio);
                money = money - musicPrice;
                musicUpgrades++;
                audio[1].Play(); //cha-ching
            }
            else
            {
                GetComponents(audio);
                audio[0].Play(); //error
            }
        }

        //Start Next Day
        if( Input.GetKeyDown(KeyCode.Alpha4))
        {
            pressedFour = true;
            Inn.playerScore_net = money;
            musicFadeInTrigger = true;
			StartCoroutine(performFade2());
        }
    }

	IEnumerator performFade2 ()
	{
		// fade out the scene
		float fadeTime = GameObject.Find("ScreenFader").GetComponent<SceneFade>().BeginFade(1);    
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene("main");
	}




}
 