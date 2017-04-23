using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDebugger : MonoBehaviour {

	public GUIStyle HUDStyle;	// settings for the GUI

	void OnGUI () 
	{
		GUI.Label (new Rect (300, 50, 200, 100), "Stools: " + ResourceManager.resourceTable[0].debugResourceAvailability(), HUDStyle);
		GUI.Label (new Rect (300, 80, 200, 100), "Bed: " + ResourceManager.resourceTable[1].debugResourceAvailability(), HUDStyle);
		GUI.Label (new Rect (300, 110, 200, 100), "Food: " + ResourceManager.resourceTable[2].debugResourceAvailability(), HUDStyle);
		GUI.Label (new Rect (300, 140, 200, 100), "Line: " + ResourceManager.resourceTable[3].debugResourceAvailability(), HUDStyle);

	}
}
