using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXTERMINATE : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag != "Player") // Don't destroy the player
            DestroyObject (other.gameObject);
	}
}
