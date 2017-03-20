using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patronBedInteraction : MonoBehaviour {
	public GameObject bed;
	public GameObject patron;
	public GameObject patronThought;

	public float sleepTimer = 5f;

	public bool sleepyTime = false;
	public static bool needsCleaning = false;

	public Sprite messyBedSprite;
	public Sprite sleepingPatronSprite;
	public Sprite origPatronSprite;
	public Sprite happyThought;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name != "player") 
		{
			origPatronSprite = other.gameObject.GetComponent<SpriteRenderer> ().sprite;
			patron = other.gameObject;
			patronThought = patron.transform.GetChild (0).gameObject;

			other.gameObject.GetComponent<SpriteRenderer> ().sprite = sleepingPatronSprite;

			sleepyTime = true;
		}
	}

	void Update() 
	{
		if (sleepyTime == true)
		{
			if(sleepTimer != 0f) 
			{
				sleepTimer -= Time.deltaTime;
			}


			if (sleepTimer <= 0f) 
			{
				sleepTimer = 5f;

				sleepyTime = false;
				patronSatisfaction.satisfied = true;
				needsCleaning = true;

				patron.gameObject.GetComponent<SpriteRenderer> ().sprite = origPatronSprite;
				bed.gameObject.GetComponent<SpriteRenderer> ().sprite = messyBedSprite;
				patronThought.gameObject.GetComponent<SpriteRenderer> ().sprite = happyThought;

			}
		}  
	}
}
