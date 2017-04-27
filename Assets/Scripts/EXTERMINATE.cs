using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXTERMINATE : MonoBehaviour {
	private int value;
	private string request;
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Patron") { // Don't destroy the player
			request = other.GetComponent<agents>().getRequest();
			value = other.GetComponent<agents> ().getValue ();
			if (other.GetComponent<agents> ().getSatisfied ()) {
				if(request == "Bed"){
					if(StoreData.musicUpgrades>=3)
						value =  Mathf.CeilToInt(value*1.25f);
					else if(StoreData.musicUpgrades>=1)
						value= Mathf.CeilToInt(value*1.10f);
				}
				else if(request == "Food"){
					if(StoreData.musicUpgrades>=4)
						value= Mathf.CeilToInt(value*1.25f);
					else if(StoreData.musicUpgrades>=2)
						value = Mathf.CeilToInt(value*1.10f);
				}
				Debug.Log ("value: " + value);
				Inn.playerScore_now += value;
			} else
				Inn.opponentScore += Mathf.CeilToInt(value*Inn.opponentMultiplier);
			
			DestroyObject (other.gameObject);
		}
	}
}
