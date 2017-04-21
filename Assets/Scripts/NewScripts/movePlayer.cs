using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour

{
	void Update ()
	{
        // move the innkeeper left if player presses left-arrow or a
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Vector3 position = this.transform.position;
			position.x -= (float).1;
			this.transform.position = position;
		}

        // move the innkeeper right if player presses right-arrow or d
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			Vector3 position = this.transform.position;
			position.x += (float).1;
			this.transform.position = position;
		}

        // move the innkeeper up if player presses up-arrow or w
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			Vector3 position = this.transform.position;
			position.y += (float).1;
			this.transform.position = position;
		}

        // move the innkeeper down if player presses down-arrow or s
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			Vector3 position = this.transform.position;
			position.y -= (float).1;
			this.transform.position = position;
		}
	}
}