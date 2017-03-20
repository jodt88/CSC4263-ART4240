using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barSensor : MonoBehaviour {

	public static bool behindBar = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Barkeeper") 
		{
			behindBar = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Barkeeper") 
		{
			behindBar = false;
		}
	}
}
