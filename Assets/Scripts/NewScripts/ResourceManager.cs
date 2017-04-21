using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour {


	public static List<Resource> resourceTable = new List<Resource>(); 
	public static int playerScore;
	public static int opponentScore;
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
		int pos = -1;
		for (int i = 0; i < available.Count; i++) {
			if (available [i]) {
				pos=i;
				break;
			}
		}
		return pos;
	}

	public bool isEmpty(){
		bool isEmpty =true;
		if (availableLinePositionRev () > 0)
			isEmpty = false;
		return isEmpty;
	}
	public int availableLinePosition(){
		int pos = 0;
		for (int i = available.Count - 1; i >= 0; i--) {
			if (available [i]) {
				pos = i;
				break;
			}
		}
		return pos;
	}

	public int availableLinePositionRev(){
		int pos = 0;
		for (int i = available.Count - 1; i >= 0; i--) {
			if (!available [i]) {
				pos = i+1;
				break;
			}
		}
		return pos;
	}

	public string debugResourceAvailability()
	{
		string availableArray=""; 
		foreach (bool child in  available) {
			if (child)
				availableArray += " t";
			else
				availableArray += " f";
		}
		return availableArray;
	}
}



