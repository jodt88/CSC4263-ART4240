using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour {


	public static List<Resource> resourceTable= new List<Resource>();  
	public static int playerScore;
	public static int opponentScore;

	// Use this for initialization
	void Start(){

		enableBeds (StoreData.bedUpgrades);
		enableTables (StoreData.tableUpgrades);
	}
	void Awake () {
		if (Inn.day == 1)
			resourceTable = new List<Resource>(); 
		int count = 0;

		foreach (Transform child in transform) {
			Resource resource = new Resource ();
			resource.setChild (child);

			if (Inn.day == 1 || mainMenu.cheatActivated)
                resourceTable.Add(resource);
            else
                resourceTable[count] = resource;

            if (Inn.day >= 2)
				checkUpgrades ();

			if (count != 2)
				resource.setIsAvailable (child.childCount);
			else
				resource.setIsAvailableTable (child.childCount);

			count++;
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void checkUpgrades(){
		for (int i = 0; i < 2; i++) {
			if (i == 0)
				enableBeds (StoreData.bedUpgrades);
			else if (i == 1)
				enableTables (StoreData.tableUpgrades);
		}

	}

	public void initializeValues(){
		
	}

	public void enableBeds(int upgrade){
		int bedsEnabled = StoreData.initialBeds+upgrade;
		GameObject table = GameObject.Find ("Beds");
		for(int i = 0; i<bedsEnabled;i++)
			table.transform.GetChild (i).gameObject.SetActive (true);	
	}

	public void enableTables(int upgrade){
		int tablesEnabled = StoreData.initialTables+upgrade;
		GameObject table = GameObject.Find ("Tables");
		for(int i = 0; i<tablesEnabled;i++)
			table.transform.GetChild (i).gameObject.SetActive (true);
	}

    public void enableMinstrels(int upgrade){
       
        GameObject[] minstrel = GameObject.FindGameObjectsWithTag("Minstrel");
        if (upgrade > 0)
        {
            for (int i = 0; i < upgrade; i++)
            {
                minstrel[i].SetActive(true);
            }
        }
    }
}

public class Resource{
	private Transform child;
	private List<bool> available;
    private List<float> cleanTime;

	public Resource(){
		available = new List<bool>();								
	}

	public void setChild (Transform child){
		this.child = child; 
	}

	public void setIsAvailable(int length){
		for (int i = 0; i < length; i++) {
			if (child.GetChild(i).gameObject.activeSelf)
				available.Add (true);
		}
	}

	public void setIsAvailableTable(int length){
		int chairs =4;
		for (int i = 0; i < length; i++) {
			if (child.GetChild(i).gameObject.activeSelf)
				for(int j = 0;j<chairs;j++)
					available.Add (true);
		}
	}

	public void setIsAvailableUpdate(int length){
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
	//returns the position of the chair using the index set finding the next available seat.
	public Transform getChairPosition(int pos){
		Transform table = child.GetChild (pos / 4 );

		return table.GetChild (pos % 4);
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

	public void resetAvailability(){
		for (int i = 0; i < available.Count; i++) {
			available [i] = true;
		}
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
	public bool getIfAvailable(int index){
		return available [index];
		
	}

}



