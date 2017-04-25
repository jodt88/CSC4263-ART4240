using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePosition : MonoBehaviour {

	GameObject patron;
	int position;
	float poll;
	bool triggered;
	// Use this for initialization
	void Start () {
		patron = null;
		triggered = false;
		position = transform.GetSiblingIndex();
		poll=0.06f;
	}

	// Update is called once per frame
	void Update () {
		//implement timer
		poll-=Time.deltaTime;
		if (poll <= 0f) {
			if (patron != null&& !triggered) {
				if (position == 0)//if first in line
					checkIfStoolAvailable ();
				else
					checkIfNextAvailable ();
			}
			poll = .06f;
		}
	}
	//if they enter
	void OnTriggerEnter2D(Collider2D other){
		GameObject person = other.gameObject;
		//modified to ensure that if a patron is go to bed or leaving bed they will not trigger being in line
		if (person.tag == "Patron") {
			if (!person.GetComponent<agents> ().getAttended()) {
				patron = person;
			}
		}
			
		
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag =="Patron") {
			if (patron!=null && other.name== patron.name) {
				patron = null;
				triggered = false;
			}
		}
	}

	void checkIfNextAvailable(){
		if (this.gameObject.GetComponentInParent<LineManagement> ().checkNextPosition (position - 1)) {
			triggered = true;
			patron.GetComponent<agents> ().traverseLine (position);
		}
	}

	public GameObject getPatron(){
		return patron;
	}
	void checkIfStoolAvailable(){
		int spot = ResourceManager.resourceTable [0].availablePosition ();
		if(spot>=0){
			triggered = true;
			patron.GetComponent<agents> ().goToStool (spot);

		}
	}


}
