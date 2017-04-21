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
                money = money - bedPrice;
                bedUpgrades++;//cha-ching audio}
            }
            else
            {
                //error audio
            }
        }
        
        //Purchase table
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(money >= tablePrice && tableUpgrades<maxTableUpgrades)
            {
                money = money - tablePrice;
                tableUpgrades++;
                //cha-ching audio
            }
            else
            {
                //error audio
            }
        }
        
        //Purchase musician
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (money >= musicPrice && musicUpgrades<maxMusicUpgrades)
            {
                money = money - musicPrice;
                musicUpgrades++;
                //cha-ching audio
            }
            else
            {
                //error audio
            }
        }

        //Start Next Day
        if( Input.GetKeyDown(KeyCode.Alpha0))
        {
            Inn.playerScore = money;
            //SceneManager.SetActiveScene        
            //SceneManager.LoadScene("main", LoadSceneMode.Additive);
        }
    }
}
 