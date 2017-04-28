using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class movePlayer : MonoBehaviour

{
    bool isMoving = false;
    List<AudioSource> walkingSound = new List<AudioSource>();

    void Update ()
	{
        // move the innkeeper left if player presses left-arrow or a
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Vector3 position = this.transform.position;
			position.x -= (float).15;
			this.transform.position = position;
            isMoving = true;

        }

        // move the innkeeper right if player presses right-arrow or d
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			Vector3 position = this.transform.position;
			position.x += (float).15;
			this.transform.position = position;
            isMoving = true;
        }

        // move the innkeeper up if player presses up-arrow or w
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			Vector3 position = this.transform.position;
			position.y += (float).15;
			this.transform.position = position;
            isMoving = true;
        }

        // move the innkeeper down if player presses down-arrow or s
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			Vector3 position = this.transform.position;
			position.y -= (float).15;
			this.transform.position = position;
            isMoving = true;
        }

        if (!Input.anyKey)
        {
            GetComponents(walkingSound);
            walkingSound[0].Stop();
        }

        if (isMoving == true)
        {
            GetComponents(walkingSound);
            if(!walkingSound[0].isPlaying)
            {
                walkingSound[0].Play();
            }
        }
	}
}