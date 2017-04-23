using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour {

	float dtSpawn; //time since last spawn
	int count; //the number of patrons that have spawned in the day.
	string request; //the request of the patron 
	public static Queue<string> requestList = new Queue<string> ();//holds all the requests for the day  this will be used to act as a way to determine which patron will be spawned next
	public  string[] requests = {"Bed","Food","Quest"};
	public static Hashtable thoughtBubble = new Hashtable ();
    
	void Awake(){

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
		if (dtSpawn>=Timers.spawnTimer && requestList.Count>=0){
			request = requestList.Peek();//Peek is used as the list is dequeued in the actual spawn method
			int pos=ResourceManager.resourceTable [3].availablePosition ();
			if(pos>=0){
				Spawn();
			}
			else
			{
				Inn.opponentScore+=getRequestValue();
			}
			count++;
			dtSpawn = 0;
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
