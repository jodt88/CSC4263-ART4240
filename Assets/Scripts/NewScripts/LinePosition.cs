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
		poll=0.05f;
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
			poll = .1f;
		}
	}
	//if they enter
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Patron")) {
			patron = other.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Patron")) {
			if (other.name == patron.name) {
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
