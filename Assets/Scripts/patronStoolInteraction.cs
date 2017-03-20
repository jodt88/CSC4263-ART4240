using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patronStoolInteraction : MonoBehaviour {
	public GameObject patron;
	public GameObject patronThought;

	public float stoolTimer = 10f;

	public bool attended = false;
	public bool sittinTime = false;

	public Sprite sittingPatronSprite;
	public Sprite origPatronSprite;
	public Sprite unhappyThought;
	public Sprite unsureThought;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name != "player") {
			origPatronSprite = other.gameObject.GetComponent<SpriteRenderer> ().sprite;
			patron = other.gameObject;
			patronThought = patron.transform.GetChild (0).gameObject;

			other.gameObject.GetComponent<SpriteRenderer> ().sprite = sittingPatronSprite;

			sittinTime = true;
		}
	}

	void Update() 
	{
		if (sittinTime == true)
		{
			if(stoolTimer != 0f)
			{
				stoolTimer -= Time.deltaTime;
			}

			if (barSensor.behindBar == true) 
			{
				attended = true;
				patronThought.gameObject.GetComponent<SpriteRenderer> ().sprite = unsureThought;
			}

			if (stoolTimer <= 0f || barSensor.behindBar == true) 
			{
				stoolTimer = 10f;

				sittinTime = false;

				patron.gameObject.GetComponent<SpriteRenderer> ().sprite = origPatronSprite;

				if (attended == false)
					patronThought.gameObject.GetComponent<SpriteRenderer> ().sprite = unhappyThought;
			}
		}  
	}
}
