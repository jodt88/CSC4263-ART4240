using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManagement : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void updateLine(GameObject patron,int resourcePosition)
	{
		patron.GetComponent<agents> ().traverseLine (resourcePosition);	
	}

	public bool checkNextPosition(int position){
		bool available = false;
		Transform child = this.transform.GetChild (position);
		if (child.GetComponent<LinePosition> ().getPatron () == null)
			available = true;
		return available;
	}
}
