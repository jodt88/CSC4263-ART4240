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
	public bool foundNextPosition;
	private int value; 
	private bool arrived;
	private bool isLeaving;
    public Sprite femSitB; //back sitting sprite for females
    public Sprite femSitL; //leftsitting sprite for females
    public Sprite femSitR; //right sitting sprite for females
    public Sprite femSitF; //front facing sitting sprite
    public Sprite maSitB; //back sitting sprite for males
    public Sprite maSitL; //leftsitting sprite for males
    public Sprite maSitR; //right sitting sprite for males
    public Sprite maSitF; //front facing sitting sprite for males
    public Sprite happyT;
    public Sprite sadT;
    public Sprite drinkT;
    public Sprite bedT;
    public Sprite questT;
    public Sprite sleepF;
    public Sprite messBed;
    Sprite myRequest;
    Sprite myBack;
    Sprite myLeft;
    Sprite myRight;
    Sprite myFront;


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
		foundNextPosition = false;
		timer = 999999f;
		resourcePosition = 0;
		request = InstanceManager.requestList.Dequeue ();
        attended = false;
		satisfied = false;
		arrived = false;
		isLeaving = false;
		setValue ();
        if (request == "Bed") { myRequest = bedT; }
        else if (request == "Food") { myRequest = drinkT; }
        else { myRequest = questT; }

        //check for patron geder; set sitting sprites
        if (this.name.Contains("patronM"))
        {
            myBack = maSitB;
            myLeft = maSitL;
            myRight = maSitR;
            myFront = maSitF;
        }
        else
        {
            myBack = femSitB;
            myLeft = femSitL;
            myRight = femSitR;
            myFront = femSitF;
        } 

    }
	void Start () { 
		goToStool ();

	}

	// Update is called once per frame
	void Update () {
		

		//setAnimation (agent.movingDirection);
		//if timer is started
		if (activateTimer) {
			if (timer > 0f) {
				if (attended && !arrived)
					attemptRequest ();
				else if (resourceInUse == "Line") {
					if (itemIndex > 0)
						checkIfNextAvailable ();
					else
						checkIfStoolAvailable ();
				}
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
			itemIndex = ResourceManager.resourceTable [0].availablePosition ();

			if (itemIndex >= 0) {
				//makes resource unavailable to everyone else
				ResourceManager.resourceTable [0].swapAvailable (itemIndex);
				//gets vector coordinate for position 
				setResourceInUse ("Stool");
				position = ResourceManager.resourceTable [0].getPosition (itemIndex).position;
				//move to position of next resource

			} 
		}
		else {
			//Find next available line
			itemIndex = ResourceManager.resourceTable [3].availableLinePosition ();
			ResourceManager.resourceTable [3].swapAvailable (itemIndex);
			setResourceInUse("Line");
			//gets vector coordinate for position 
			position = ResourceManager.resourceTable[3].getPosition(itemIndex).position;
		}
		agent.SetDestination (position,onReached);
	}
	public void goToStool(int spot){
		itemIndex = spot;
		ResourceManager.resourceTable [0].swapAvailable (itemIndex);
		//gets vector coordinate for position 
		setResourceInUse("Stool");
		activateTimer = false;
		position = ResourceManager.resourceTable [0].getPosition (itemIndex).position;
		//move to position of next resource
		agent.SetDestination (position,onReached);
	}

	public void attemptRequest(){
		activateTimer = false;
		ResourceManager.resourceTable [0].swapAvailable (itemIndex);
		resetInteractionSprite (0, itemIndex);
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
			resourceInUse = "none";
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
                setInteractionSprite ();
                //ResourceManager.resourceTable[0].getPosition(resourcePosition).GetComponent<SpriteRenderer>().enabled = false;
                break;
			case "Food":
				setTimer (Timers.foodTimer);
                this.GetComponent<SpriteRenderer>().enabled = false;
                if (ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).name == "TopChair")
                    {
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = myFront;
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    }
                if (ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).name == "BottomChair")
                    {
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = myBack;
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    }
                if (ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).name == "LeftChair")
                    {
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = myLeft;
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    }
                if (ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).name == "RightChair")
                    {
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = myRight;
                        ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    }
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
                //this.GetComponent<SpriteRenderer>().enabled = false;
                //ResourceManager.resourceTable[resourcePosition].getPosition(resourcePosition).GetChild(0).GetComponent<SpriteRenderer>().sprite = sleepF;
                //ResourceManager.resourceTable[resourcePosition].getPosition(resourcePosition).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                arrived = true;
                this.GetComponent<SpriteRenderer>().enabled = false;
                ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = sleepF;
                ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
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
            ResourceManager.resourceTable[0].swapAvailable(itemIndex);
            //ResourceManager.resourceTable[0].getPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = emtStool;
            ResourceManager.resourceTable[0].getPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            ResourceManager.resourceTable[0].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

        }

        if (resourceInUse == "Food"&&satisfied)
        {
            //ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = emtStool;
			ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }

        if (resourceInUse == "Bed"&&satisfied)
        {
            ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = messBed;
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

		if (resourceInUse == "Line") {
			if (!ResourceManager.resourceTable [3].getIfAvailable (itemIndex)) {
				ResourceManager.resourceTable [3].swapAvailable (itemIndex);
			}
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
		if(ResourceManager.resourceTable[3].getIfAvailable(spot-1))
			ResourceManager.resourceTable [3].swapAvailable (spot-1);
		if(!ResourceManager.resourceTable[3].getIfAvailable(spot))
			ResourceManager.resourceTable [3].swapAvailable (spot);

		itemIndex = spot - 1;
		position = ResourceManager.resourceTable[3].getPosition(itemIndex).position;

		agent.SetDestination (position);


	}

    public void setInteractionSprite()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).name = this.name;
        ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().sprite = myBack;
        ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        ResourceManager.resourceTable[0].getPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().sprite = myRequest;
        ResourceManager.resourceTable[0].getPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }


    public void resetInteractionSprite(int resource, int pos){
		this.transform.GetChild(resource).gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<SpriteRenderer>().enabled = true;
		ResourceManager.resourceTable [resource].getPosition (pos).name = this.name;
		//ResourceManager.resourceTable[resource].getPosition(pos).GetComponent<SpriteRenderer>().sprite = image;
        ResourceManager.resourceTable[0].getPosition(pos).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        ResourceManager.resourceTable[resource].getPosition(pos).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

	public void setAnimation(Vector2 movingDirection){
		float direction = Mathf.Abs (movingDirection.x) - Mathf.Abs (movingDirection.y);
		if (direction >= 0f && movingDirection.x > 0f) {
			//set animmation to walking right
					//Debug.Log("Set right animation");
		} else if (direction < 0f && movingDirection.y > 0f) {
				//set animation to walking up
					//Debug.Log("Set up animation");
		} else if (direction >= 0f && movingDirection.x < 0f) {
					//set animation to walking left
					//Debug.Log("Set left animation");
		}else if (direction < 0f && movingDirection.y <0f){
					//set animation to walking down
					//Debug.Log("Set down animation");
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		GameObject person = other.gameObject;
		//modified to ensure that if a patron is go to bed or leaving bed they will not trigger being in line
		if (other.tag == "Line") {
			if (!this.getAttended()) {
				foundNextPosition = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		GameObject person = other.gameObject;
		if (other.tag =="Line") {
			if (!this.getAttended()&&foundNextPosition) {
				foundNextPosition = false;
			}
		}
	}

	void checkIfStoolAvailable(){
		int stoolPos = ResourceManager.resourceTable [0].availablePosition ();
		if(stoolPos>=0&&!foundNextPosition){
			foundNextPosition = true;
			ResourceManager.resourceTable [3].swapAvailable (0);
			goToStool (stoolPos);

		}
	}

	void checkIfNextAvailable(){
		if (ResourceManager.resourceTable[3].getIfAvailable(itemIndex-1)&&!foundNextPosition) {
			foundNextPosition = true;
			traverseLine (itemIndex);
		}
	}

	void setResourceSprite(){
		if (resourceInUse == "Stool")
		{
			ResourceManager.resourceTable[0].swapAvailable(itemIndex);
			//ResourceManager.resourceTable[0].getPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = emtStool;
		}

		else if (resourceInUse == "Food")
		{
			//ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = emtStool;
			ResourceManager.resourceTable[resourcePosition].getChairPosition(itemIndex).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		}

		else if (resourceInUse == "Bed")
		{
			ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
			ResourceManager.resourceTable[resourcePosition].getPosition(itemIndex).GetComponent<SpriteRenderer>().sprite = messBed;
		}

	}

	void setSatisfiedSprite(){
		if (satisfied){
			//happyEmote
			this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = happyT;
		}
		else
		{
			//sadEmote
			this.transform.GetChild(0).gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = sadT;
		}
				
	}
}