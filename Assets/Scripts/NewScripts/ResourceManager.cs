using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour {
	

	public static List<Resource> resourceTable = new List<Resource>();
	//public static List<LinePosition> line = new List<LinePosition>(); 
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
		
}

/*public class LinePosition{
	private Vector2 position;
	private string person;

	public LinePosition(){
		person = "";
		position = new Vector2 ();
	}

	public void setPerson(string person){
		this.person = person;
	}

	public void setPosition(Vector2 position)
	{
		this.position = position;
	}

	public string getPerson(){
		return person;
	}

	public Vector2 getPosition(){
		return position;
	}

	
}*/