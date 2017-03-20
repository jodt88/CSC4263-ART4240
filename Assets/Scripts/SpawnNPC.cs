using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour {

    // Use this for initialization
    float time;

   public GameObject patronInstance;

    void Start ()
    {
        time = 0;
	 

    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (time >= 10f)
            Spawn();
	}

    void Spawn()
    {
        time = 0;
		patronInstance = Instantiate(Resources.Load ("patron"),transform.position,transform.rotation)as GameObject;
        //Instantiate(patron, transform.position, transform.rotation);
    }
}
