using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreData : MonoBehaviour
{
    int money;
    public static int bedUpgrades;
    public static int tableUpgrades;
    public static int musicUpgrades;
    int maxBedUpgrades = 6;
    int maxTableUpgrades = 6;
    int maxMusicUpgrades = 5;
    int bedPrice = 200;
    int tablePrice = 100;
    int musicPrice = 300;

    List<AudioSource> audio = new List<AudioSource>();

    public static bool musicFadeInTrigger; //Utilized by MusicPlayer Script

// Use this for initialization
    void Start()
    {
        money = Inn.playerScore;
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
            if(money >= tablePrice && tableUpgrades<maxTableUpgrades)
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
            if (money >= musicPrice && musicUpgrades<maxMusicUpgrades)
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
            Inn.playerScore = money;
            musicFadeInTrigger = true;


            //JODY  JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY JODY 
            //Can you transistion back to the game here, starting the new day?
            //Thanks, Greg

        }
    }
}
 