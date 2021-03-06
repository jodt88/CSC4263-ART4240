﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
	GameObject tavernObj;
    GameObject sensorObj;
    public Sprite cleanBed;
    List<AudioSource> audios = new List<AudioSource>();
    // Use this for initialization
    void Start ()
    {
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
					interactPatron();
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
        //checks that left button was clicked while behind bar
        GetComponents(audios);
		if (tavernObj.name.Contains("patron")&&barSensor.behindBar)
        {
			GameObject patron = findPatron ();

			if (patron != null)
            {
                patron.GetComponent<agents>().wasAttended();
                if (patron.GetComponent<agents>().getRequest() == "Bed")
                    audios[1].Play();
                else
                    audios[2].Play();

            }

	    }
	}


    void interactTable()
    {
        if(tavernObj.transform.parent.gameObject.GetComponent<TableSensor>().getByTable())
        {
            ResourceManager.resourceTable[2].swapAvailable(tavernObj.transform.GetSiblingIndex()+ tavernObj.transform.parent.transform.GetSiblingIndex()*4);
            GetComponents(audios);
            audios[3].Play();
            tavernObj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void interactBed()
    {
        if (tavernObj.transform.GetChild(0).gameObject.GetComponent<BedSensor>().getByBed())
        {
            ResourceManager.resourceTable[1].swapAvailable(tavernObj.transform.GetSiblingIndex());
            GetComponents(audios);
            audios[3].Play();
            tavernObj.GetComponent<SpriteRenderer>().sprite = cleanBed;
        }
    }


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



