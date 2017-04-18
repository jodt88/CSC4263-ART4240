using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class agents : MonoBehaviour {

	// Use this for initialization

	private PolyNavAgent _agent;//the means of map traversal utilizing a*
	private string resourceInUse; //resource currently in use by agent
	private Vector2 position;// Used to denote the actual v3 position the patron will go to.
	private int resourcePosition;//Used to denote which specific resource out of the group the patron will access 
	private float timer;//used to determine remaining time till patron will finish using a resource. 
	private bool activateTimer; //used to trigger the timer for the player
	private string request; //resource patron wants to use
	private bool attended; //has the innkeeper acknowledged the patron
	private bool satisfied; //was the patron able to use their desired resource
	private int value; 
	private bool arrived;

	public PolyNavAgent agent{
		get
		{
			if (_agent == null)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}
	void Awake(){
		activateTimer = false;
		timer = 999999f;
		resourcePosition = 0;
		request = InstanceManager.requestList.Dequeue ();
		attended = false;
		satisfied = false;
		arrived = false;
		setValue ();

	}
	void Start () { 
		goToStool ();
	}

	// Update is called once per frame
	void Update () {

		//if timer is started
		if (activateTimer) {
			if (timer > 0f) {
				if (attended&&!arrived)
					attemptRequest ();
				timer -= Time.deltaTime;
			} else
				activateTimer = false;
		} else if (!activateTimer && timer <= 0f) {
			leaveTavern (satisfied);
		} 

	}

	public bool getAttended(){
		return attended;
	}

	public int getValue(){
		return value;
	}

	public string getRequest(){
		return request; 
	}

	public bool getSatisfied(){
		return satisfied;
	}

	public void wasAttended(){
		attended = true;
	}

	public void wasSatisfied(){
		satisfied = true;
	}

	void goToStool(){
		//verifies if there is a stool available
		resourcePosition = ResourceManager.resourceTable [0].availablePosition ();

		if (resourcePosition < 0) {
			//Find next available line
			resourcePosition = ResourceManager.resourceTable [3].availablePosition ();
			ResourceManager.resourceTable [3].swapAvailable (resourcePosition);
			resourceInUse = "Line";
			//gets vector coordinate for position 
			position = ResourceManager.resourceTable[3].getPosition(resourcePosition).position;
		} else {
			//makes resource unavailable to everyone else
			ResourceManager.resourceTable [0].swapAvailable (resourcePosition);
			//gets vector coordinate for position 
			resourceInUse = "Stool";
			position = ResourceManager.resourceTable [0].getPosition (resourcePosition).position;
			//move to position of next resource
		}
		agent.SetDestination (position,onReached);
	}

	public void attemptRequest(){
		activateTimer = false;
		switch (request) {
		case "Bed":
			resourcePosition = 1;
			break;
		case "Food":
			resourcePosition = 2;
			break;
		case "Quest":
			resourcePosition = 3;
			break;
		}

		int pos = ResourceManager.resourceTable [resourcePosition].availablePosition ();
		if(pos<0)
		{
			//gets a fakeposition near the first resource 
			position = ResourceManager.resourceTable[resourcePosition].getPosition(pos).position;
			position.x += 2;
			agent.SetDestination(position,noResource);
		}
		else{
			position = ResourceManager.resourceTable[resourcePosition].getPosition(0).position;
			resourceInUse = request;
			agent.SetDestination (position, onReached);
		}
	}

	public void onReached(bool success){
		if (success){ 
			switch(resourceInUse)
			{
			case "Line":
				setTimer(Timers.lineTimer);	
				break;
			case "Stool": 
				setTimer(Timers.stoolTimer);
				break;
			case "Food":
				setTimer (Timers.foodTimer);
				arrived = true;
				wasSatisfied ();
				break;
			case "Quest":
				setTimer (Timers.questTimer);
				arrived = true;
				wasSatisfied ();
				break;
			case "Bed":
				setTimer (Timers.bedTimer);
				arrived = true;
				wasSatisfied ();
				break;
			}		
		}	
	}


	//activates the timer and sets time patron will wait before leaving unsatisfied
	public void noResource(bool success){
		if(success){
			activateTimer = true;
			setTimer(Timers.noResourceTimer);
		}
	}

	public void leaveTavern(bool satisfied){
		var pos = GameObject.Find ("PatronDespawner").transform.position;	
		if(satisfied){
			//happyEmote
		}
		else
		{
			//sadEmote
		}
		agent.SetDestination (pos);
	}

	public void destroyCallback(bool success){
		if (success) {
			//check
			DestroyObject (this.gameObject);
		}
	}

	public void setTimer(float time){
		timer = time;
		activateTimer = true;
	}

	public string getResourceInUse(){
		return resourceInUse;
	}

	public void setValue(){
		switch (request) {
		case "Bed":
			value = Inn.bedValue;
			break;
		case "Food":
			value = Inn.foodValue;
			break;
		case "Quest":
			value = Inn.questValue;
			break;
		}
	}
}
