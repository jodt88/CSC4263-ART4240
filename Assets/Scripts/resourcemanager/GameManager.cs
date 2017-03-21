using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
	

	public static List<Resource> resourceTable = new List<Resource>();

	// Use this for initialization
	void Start(){
		
	}
	void Awake () {
		//popluates the hash table with each resource
		foreach (Transform child in transform) {
			Resource resource = new Resource ();
			resource.setChild (child);
			resource.setIsAvailable (child.childCount);
			resourceTable.Add (resource);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//creates a bool list of of size length all instanciated to true.
	

}

public class Resource{
	private Transform child;
	private List<bool> available;

	public Resource(){
		available = new List<bool>();								
	}

	public void setChild (Transform child){
		this.child = child; 
	}

	public void setIsAvailable(int length){
		for (int i = 0; i < length; i++) {
			available.Add (true);
		}
	}

	public void swapAvailable(int pos){
		available [pos] = !available [pos];
	}

	public Transform getPosition(int pos){
		return child.GetChild (pos);
	}
	public string getTag(int pos){
		return child.GetChild (pos).tag;
	}
	public int availablePosition(){
		bool isAvailable = false;
		int i;
		for (i = 0; i < available.Count; i++) {
			if (available [i]) {
				isAvailable = true; 
				break;
			}
		}
		if (!isAvailable)
			i = -1;

		return i;
	}
		
}