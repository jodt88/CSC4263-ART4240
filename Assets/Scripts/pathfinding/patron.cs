using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class patron : MonoBehaviour {

	public List<Vector2> WPoints = new List<Vector2>();
	private PolyNavAgent _agent;
	private string destinationType;
	private string[] requestList = new string[3] {"Bed", "Food","Quest"};
	string request;
	Sprite unhappyThought;
	Sprite happyThought;
	Vector3 position;
	private int resourcePosition; 

	private float timer;
	private bool activateTimer = false;
	private bool attended = false; 
	private bool requestFulfilled=false;


	public PolyNavAgent agent{
		get
		{
			if (_agent == null)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}
	private bool satisfied;

	void Awake(){
		unhappyThought= Resources.Load<Sprite>("patron unhappy thought");
		happyThought = Resources.Load<Sprite> ("patron happy thought");
	}


	void Start () {
		aiEvents ("Stool");

	}

	void aiEvents(string destination){
		int res=-1;
		Transform resource=null;
		if (destination == "Stool") {
			res = 0;

		}
		else if (destination == "Bed") {
			res = 1;
		}

		resourcePosition = GameManager.resourceTable [res].availablePosition ();
		if (resourcePosition < 0) {
			if (destination == "Stool") {
				
				position = this.transform.position;
				//like this for demos sake will just use bottom function 
				timer = 3f;
				activateTimer = true; 
				//destinationType = "Line";

			} else if (destination == "Bed") {
				//go to room then come back 
				resource = GameManager.resourceTable [res].getPosition (0);
				destinationType = GameManager.resourceTable [res].getTag (0);
				position = resource.position + new Vector3 (3, 0, 0);
			}
			agent.SetDestination (position,unSatisfiedCallBack);

		} else {
			//gets the specific resource
			position = GameManager.resourceTable [res].getPosition (resourcePosition).position;
			//gets destination type 
			destinationType = GameManager.resourceTable [res].getTag (resourcePosition);
			//swaps the boolean value bool at index of boolean array
			GameManager.resourceTable [res].swapAvailable (resourcePosition);
			agent.SetDestination (position, NavCallback);
		}
		//makes patron walk to destination and upon arriving will perform callback method

	}

	void NavCallback(bool succeed){
		//triggers if patron successfully reaches goal.
		if (succeed) {
			if (destinationType == "Stool") {
				timer = 100f;
				activateTimer = true; 
				request = requestList [0];
			} else if (destinationType == "Bed") {
				timer = 7f;
				activateTimer = true; 
				requestFulfilled = true;
			}
		} else {
			
		}

	}

	void unSatisfiedCallBack(bool succeed){
		if (succeed) {
			var pos = GameObject.Find ("EXTERMINATE!").transform.position;
			GameObject thought = transform.GetChild (0).gameObject; 
			thought.GetComponent<SpriteRenderer> ().sprite = unhappyThought;
			agent.SetDestination (pos, DestroyCallback);
		}
	}
	void DestroyCallback(bool succeed){
		if (succeed) {
			DestroyObject (this.gameObject);
		}
	}


	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);

			if (hit != null && hit.collider != null&& barSensor.behindBar) {
				if( this.gameObject==GameObject.Find (hit.collider.gameObject.name))
					{
						attended = true;
					}
			}
		}
		if (activateTimer) {
			//if there is remaining time and patron hasnt been attended
			if (timer > 0f&&!attended) {
				timer -= Time.deltaTime;
			} 
			else {
				//if patron was attended 
				if (attended && !requestFulfilled) {
					activateTimer = false;
					aiEvents ("Bed");
				} else if (attended && requestFulfilled&&timer>0f) {

						timer -= Time.deltaTime;
					}
				else if(attended && requestFulfilled&&timer<=0f){
					activateTimer = false;
					var pos = GameObject.Find ("EXTERMINATE!").transform.position;
					GameObject thought =  transform.GetChild(0).gameObject; 
					thought.GetComponent<SpriteRenderer> ().sprite = happyThought;
					agent.SetDestination (pos,DestroyCallback);
				}
				else{
					var pos = GameObject.Find ("EXTERMINATE!").transform.position;
					GameObject thought =  transform.GetChild(0).gameObject; 
					thought.GetComponent<SpriteRenderer> ().sprite = unhappyThought;
					agent.SetDestination (pos,DestroyCallback);
				}
			}
		}
	}
}




