using UnityEngine;
using System.Collections;

public class movePatron : MonoBehaviour

{
	void Update ()
	{
		if (Input.GetKey(KeyCode.A))
		{
			Vector3 position = this.transform.position;
			position.x -= (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.D))
		{
			Vector3 position = this.transform.position;
			position.x += (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.W))
		{
			Vector3 position = this.transform.position;
			position.y += (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.S))
		{
			Vector3 position = this.transform.position;
			position.y -= (float).1;
			this.transform.position = position;
		}
	}
}