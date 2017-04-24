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
	private int itemIndex; //the index of the resource the patron will occupy
	private float timer;//used to determine remaining time till patron will finish using a resource. 
	private bool activateTimer; //used to trigger the timer for the player
	private string request; //resource patron wants to use
	private bool attended; //has the innkeeper acknowledged the patron
	private bool satisfied; //was the patron able to use their desired resource
	private int value; 
	private bool arrived;
	private bool isLeaving;
    public Sprite occuStool;
    public Sprite emtStool;
    public Sprite happyT;
    public Sprite sadT;
    public Sprite drinkT;
    public Sprite bedT;
    public Sprite questT;


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
		isLeaving = false;
		setValue ();
        if (request == "Bed") { this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = bedT; }
        else if (request == "Food") { this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = drinkT; }
        else { this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = questT; }

    }
	void Start () { 
		goToStool ();

	}

	// Update is called once per frame
	void Update () {
		

		setAnimation (agent.movingDirection);
		//if timer is started
		if (activateTimer) {
			if (timer > 0f) {
				if (attended&&!arrived)
					attemptRequest ();
				timer -= Time.deltaTime;
            } else
				activateTimer = false;
		} else if (!activateTimer && timer <= 0f&&!isLeaving) {
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

	public void goToStool(){
		//verifies if the line is empty
		if (ResourceManager.resourceTable [3].isEmpty ()&&ResourceManager.resourceTable[0].availablePosition()>-1) {
			//check for stool availability
			resourcePosition = ResourceManager.resourceTable [0].availablePosition ();

			if (resourcePosition >= 0) {
				//makes resource unavailable to everyone else
				ResourceManager.resourceTable [0].swapAvailable (resourcePosition);
				//gets vector coordinate for position 
				setResourceInUse ("Stool");
				position = ResourceManager.resourceTable [0].getPosition (resourcePosition).position;
				//move to position of next resource

			} 
		}
		else {
			//Find next available line
			resourcePosition = ResourceManager.resourceTable [3].availableLinePosition ();
			ResourceManager.resourceTable [3].swapAvailable (resourcePosition);
			setResourceInUse("Line");
			//gets vector coordinate for position 
			position = ResourceManager.resourceTable[3].getPosition(resourcePosition).position;
		}
		agent.SetDestination (position,onReached);
	}
	public void goToStool(int spot){
		resourcePosition = spot;
		ResourceManager.resourceTable [0].swapAvailable (spot);
		//gets vector coordinate for position 
		setResourceInUse("Stool");
		activateTimer = false;
		position = ResourceManager.resourceTable [0].getPosition (spot).position;
		//move to position of next resource
		agent.SetDestination (position,onReached);
	}

	public void attemptRequest(){
		activateTimer = false;
		ResourceManager.resourceTable [0].swapAvailable (resourcePosition);
		resetInteractionSprite (0, resourcePosition, emtStool);
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

		itemIndex = ResourceManager.resourceTable [resourcePosition].availablePosition ();
		if(itemIndex<0)
		{
			//gets a fakeposition near the first resource 
			if (resourcePosition == 2) {
				this.position = ResourceManager.resourceTable [resourcePosition].getChairPosition (0).position;
				this.position.x -= 2;
			} else {
				this.position = ResourceManager.resourceTable [resourcePosition].getPosition (0).position;
				this.position.x += 2;
			}
			this.agent.SetDestination(position,noResource);
		}
		else{
			if (resourcePosition == 2)
				position = ResourceManager.resourceTable [resourcePosition].getChairPosition (itemIndex).position;
			else
				position = ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).position;

			ResourceManager.resourceTable [resourcePosition].swapAvailable (itemIndex);
			setResourceInUse(request);
			agent.SetDestination (position, onReached);
		}
	}

	public void onReached(bool success){
		if (success){    
            switch (resourceInUse)
			{
			case "Line":
				setTimer(Timers.lineTimer);	
				break;
			case "Stool": 
				setTimer (Timers.stoolTimer);
				setInteractionSprite (0,resourcePosition,occuStool);
                //ResourceManager.resourceTable[0].getPosition(resourcePosition).GetComponent<SpriteRenderer>().enabled = false;
                break;
			case "Food":
				setTimer (Timers.foodTimer);
                this.GetComponent<SpriteRenderer>().enabled = false;
                ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = occuStool;
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
		if(success&&!arrived){
			arrived = true;
			activateTimer = true;
			setTimer(Timers.noResourceTimer);
		}
	}

	public void leaveTavern(bool satisfied){
        this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        var pos = GameObject.Find ("PatronDespawner").transform.position;
		isLeaving = true;
       
		if (resourceInUse == "Stool")
        {
            ResourceManager.resourceTable[0].swapAvailable(resourcePosition);
            ResourceManager.resourceTable[0].getPosition(resourcePosition).GetComponent<SpriteRenderer>().sprite = emtStool;
        }

        if (resourceInUse == "Food")
        {
            ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = emtStool;
            //ResourceManager.resourceTable[resourcePosition].getChairPosition(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
			ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

        if (satisfied){
            //happyEmote
            this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = happyT;
        }
		else
		{
            //sadEmote
            this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = sadT;
        }
		agent.SetDestination (pos);
	}
		
	public void setTimer(float time){
		timer = time;
		activateTimer = true;
	}

	public string getResourceInUse(){
		return resourceInUse;
	}

	public void setResourceInUse(string resource){
		resourceInUse = resource;
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
	public void traverseLine(int spot){
		ResourceManager.resourceTable [3].swapAvailable (spot);
		position = ResourceManager.resourceTable[3].getPosition(spot-1).position;
		ResourceManager.resourceTable [3].swapAvailable (spot-1);
		agent.SetDestination (position);


	}

	public void setInteractionSprite(int resource,int pos,Sprite image){
		this.transform.GetChild(resource).gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponent<SpriteRenderer>().enabled = false;
		ResourceManager.resourceTable [resource].getPosition (pos).name = this.name;
		ResourceManager.resourceTable[resource].getPosition(pos).GetComponent<SpriteRenderer>().sprite = image;
	}

	public void resetInteractionSprite(int resource, int pos, Sprite image){
		this.transform.GetChild(resource).gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<SpriteRenderer>().enabled = true;
		ResourceManager.resourceTable [resource].getPosition (pos).name = this.name;
		ResourceManager.resourceTable[resource].getPosition(pos).GetComponent<SpriteRenderer>().sprite = image;
	}

	public void setAnimation(Vector2 movingDirection){
		float direction = Mathf.Abs (movingDirection.x) - Mathf.Abs (movingDirection.y);
		if (direction >= 0f && movingDirection.x > 0f) {
			//set animmation to walking right
					Debug.Log("Set right animation");
		} else if (direction < 0f && movingDirection.y > 0f) {
				//set animation to walking up
					Debug.Log("Set up animation");
		} else if (direction >= 0f && movingDirection.x < 0f) {
					//set animation to walking left
					Debug.Log("Set left animation");
		}else if (direction < 0f && movingDirection.y <0f){
					//set animation to walking down
					Debug.Log("Set down animation");
		}
	}

				
}
