using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickDetect : MonoBehaviour {

	public static bool thoughtClicked = false;

	void OnMouseDown () {
		thoughtClicked = true;
	}
}
