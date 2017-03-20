using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour

{
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 position = this.transform.position;
			position.x -= (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 position = this.transform.position;
			position.x += (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 position = this.transform.position;
			position.y += (float).1;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Vector3 position = this.transform.position;
			position.y -= (float).1;
			this.transform.position = position;
		}
	}
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            pos.y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            pos.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            pos.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            pos.x += 1;

        transform.position = Vector3.MoveTowards(transform.position, pos, (float)1.5 * Time.deltaTime);
    }

}*/