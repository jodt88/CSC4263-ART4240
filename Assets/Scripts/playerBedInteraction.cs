using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBedInteraction : MonoBehaviour {
	public GameObject bed;

	public float cleanTimer = 3f;

	public Sprite cleanBedSprite;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Barkeeper" && patronBedInteraction.needsCleaning == true) 
		{
			if(cleanTimer != 0f) 
			{
				cleanTimer -= Time.deltaTime;
			}


			if (cleanTimer <= 0f) 
			{
				cleanTimer = 3f;

				bed.gameObject.GetComponent<SpriteRenderer> ().sprite = cleanBedSprite;
			}
		}
	}
}

