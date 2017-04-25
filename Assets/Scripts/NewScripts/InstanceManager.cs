using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour {

	float dtSpawn; //time since last spawn
	int count; //the number of patrons that have spawned in the day.
	int spawnTotal;
	string request; //the request of the patron 
	public static Queue<string> requestList = new Queue<string> ();//holds all the requests for the day  this will be used to act as a way to determine which patron will be spawned next
	public  string[] requests = {"Bed","Food","Quest"};
	public static Hashtable thoughtBubble = new Hashtable ();
    
	void Awake(){
		initializePatronSpawn ();
		for(int i = 0;i<50;i++)
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
	void initializePatronSpawn(){
		switch (Inn.day) {
		case 1:
			Timers.spawnTimer = 5f;
			spawnTotal = 53;
			break;
		case 2:
			Timers.spawnTimer = 4.5f;
			spawnTotal = 59;
			break;
		case 3:
			Timers.spawnTimer = 4f;
			spawnTotal = 67;
			break;
		case 4:
			Timers.spawnTimer = 3.5f;
			spawnTotal = 67;
			break;
		case 5:
			Timers.spawnTimer = 3.0f;
			spawnTotal = 89;
			break;
		case 6:
			Timers.spawnTimer = 2.5f;
			spawnTotal = 107;
			break;
		case 7:
			Timers.spawnTimer = 2f;
			spawnTotal = 134;
			break;
		}
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
		//spawns the patron prefab 
		patronInstance = Instantiate(Resources.Load("patron"),transform.position,transform.rotation) as GameObject;
		//gives each instace of patron a unique name
		patronInstance.name = "patron"+request + count;   
	}

}
