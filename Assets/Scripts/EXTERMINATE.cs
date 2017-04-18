using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXTERMINATE : MonoBehaviour {
	private int value;
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Patron") { // Don't destroy the player
			value = other.GetComponent<agents> ().getValue ();
			if (other.GetComponent<agents> ().getSatisfied ()) {
				Inn.playerScore += value;
			} else
				Inn.opponentScore += value;
			DestroyObject (other.gameObject);
		}
	}
}
