﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
	GameObject tavernObj;
    GameObject sensorObj;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
            tavernObj = getInteractionObject ();
			if (tavernObj != null) {
				switch (tavernObj.tag) {
				case "Bed":
                    interactBed();
                    break;
				case "Food":
					break;
				case "Stool":
					interactPatron ();
					break;
                case "Chair":
                    interactTable();
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
		if (tavernObj.name.Contains("patron")&&barSensor.behindBar)
        {
			GameObject patron = findPatron ();

			if (patron != null) {
				patron.GetComponent<agents> ().wasAttended ();
            }

	    }
	}


    void interactTable()
    {
        if(tavernObj.transform.parent.gameObject.name == sensorObj.name)
        {
            ResourceManager.resourceTable[2].swapAvailable(tavernObj.transform.GetSiblingIndex()+sensorObj.transform.GetSiblingIndex()*4);
            tavernObj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void interactBed() { }


    GameObject findPatron () {
		var objects = GameObject.FindGameObjectsWithTag("Patron");
		foreach(GameObject patron in objects){
			if (patron.name == tavernObj.name)
				return patron;
		}
		return null;
	}

    public void setSensorObject(GameObject sensor)
    {
        sensorObj = sensor;
    }
}



