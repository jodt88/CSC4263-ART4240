using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
	GameObject gameObject;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			gameObject = getInteractionObject ();
			if (gameObject != null) {
				switch (gameObject.tag) {
				case "Bed":
					break;
				case "Patron":
					interactPatron ();
					break;
				case "Food":
					break;
				}
			}
		}

	}

	GameObject getInteractionObject(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);
		if (hit != null && hit.collider != null)
			return hit.collider.gameObject;
		else
			return null;
	}

	void interactPatron(){
		//checks that left button was clicked while behind bard
		if (gameObject.GetComponent<agents>().getResourceInUse()=="Stool"&&barSensor.behindBar) {
					//will just auto interact with patron this will be replaced by ui. 
					gameObject.GetComponent<agents> ().wasAttended ();
				}
	}
}



