using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour {

	float dtSpawn; //time since last spawn
	int count; //the number of patrons that have spawned in the day.
	int spawnTotal;
	float spawnRateBonus=0f;
	string request; //the request of the patron 
	public static Queue<string> requestList = new Queue<string> ();//holds all the requests for the day  this will be used to act as a way to determine which patron will be spawned next
	public string[] requests = {"Bed","Food","Quest"};
    public string[] genderList = { "patron, patronM" };
    public string patronPrefab;
	public static Hashtable thoughtBubble = new Hashtable ();
    public Sprite maleSprt;
    
	void Awake(){
		if (StoreData.musicUpgrades == 5)
			spawnRateBonus = .5f;
		initializeDayValues();
		Debug.Log ("Total that will spawn: " + spawnTotal.ToString());
		for(int i = 0;i<spawnTotal;i++)
		{
			//populates the queue with the order of requests that will be for the day this will be subject to change on later implementation
			requestList.Enqueue(requests[Random.Range(0,2)]);

		}
	}

	public GameObject patronInstance;

	void Start ()
	{
		dtSpawn = 0;
		count = 0;

	}


	void Update ()
	{
		dtSpawn += Time.deltaTime;

		//checks if last position in line is available.
		if (dtSpawn>=Timers.spawnTimer && count<spawnTotal){
			request = requestList.Peek();//Peek is used as the list is dequeued in the actual spawn method
			int pos=ResourceManager.resourceTable [3].availablePosition ();
			if(pos>=0){
				Spawn();
			}
			else{
				Inn.opponentScore+=getRequestValue();
			}
			count++;
			dtSpawn = 0;
		}



	}
	void initializeDayValues(){
		switch (Inn.day) {
		case 1:
			Timers.spawnTimer = 4.5f-spawnRateBonus;
			Inn.opponentMultiplier = 1f;
			break;
		case 2:
			Timers.spawnTimer = 4.25f-spawnRateBonus;
			Inn.opponentMultiplier = 1.25f;
			break;
		case 3:
			Timers.spawnTimer = 3.75f-spawnRateBonus;
			Inn.opponentMultiplier = 1.50f;
			break;
		case 4:
			Timers.spawnTimer = 3.25f-spawnRateBonus;
			Inn.opponentMultiplier = 2.0f;
			break;
		case 5:
			Timers.spawnTimer = 2.75f-spawnRateBonus;
			Inn.opponentMultiplier = 2.50f;
			break;
		case 6:
			Timers.spawnTimer = 2.25f-spawnRateBonus;
			Inn.opponentMultiplier = 2.75f;
			break;
		case 7:
			Timers.spawnTimer = 1.75f-spawnRateBonus;
			Inn.opponentMultiplier = 3f;
			break;
		}
		Debug.Log (Timers.spawnTimer);
		spawnTotal = (int)calculateSpawnCount (Timers.spawnTimer);
	}

	public float calculateSpawnCount(float spawnRate){
		float timeInDay = 60f * (float)Inn.hour + (float)Inn.minute;
		return ((timeInDay - (Timers.lineTimer + Timers.stoolTimer + Timers.bedTimer + 2f)) / spawnRate);
	}

	int getRequestValue()
	{ 
		int value = 0;
		switch(request){
		case "Bed":
			value = Inn.bedValue;
			break;
		case "Quest":
			value= Inn.questValue;
			break;
		case "Food":
			value = Inn.foodValue;
			break;
		}

		return value;
	}

	void Spawn()
	{
        int random = Random.Range(0, 100) % 2;
        
        if(random ==1)
            patronInstance = Instantiate(Resources.Load("patronM"),transform.position,transform.rotation) as GameObject;
        else
            patronInstance = Instantiate(Resources.Load("patron"), transform.position, transform.rotation) as GameObject;
        //   patronInstance.GetComponent<SpriteRenderer>().sprite = maleSprt;
        //gives each instace of patron a unique name
        patronInstance.name = "patron"+request + count;   
	}

}
