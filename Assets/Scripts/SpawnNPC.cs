using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour {

    // Use this for initialization
    float time;
	int count;
   public GameObject patronInstance;

    void Start ()
    {
        time = 0;
	 
		count = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
		if (time >= 3f&&count<4)
            Spawn();
	}

    void Spawn()
    {
        time = 0;
		patronInstance = Instantiate(Resources.Load ("patron"),transform.position,transform.rotation)as GameObject;
		//gives each instace of patron a unique name
		patronInstance.name = "patron" + count;
		count++;
        //Instantiate(patron, transform.position, transform.rotation);
    }
}
