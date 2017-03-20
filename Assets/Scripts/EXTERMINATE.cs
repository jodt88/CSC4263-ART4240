using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXTERMINATE : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) 
	{
		DestroyObject (other.gameObject);
	}
}
