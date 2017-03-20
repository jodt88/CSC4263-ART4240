using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour {

    // Use this for initialization
    float time;

    public GameObject patron;

    void Start ()
    {
        time = 0;
        patron = GameObject.FindGameObjectWithTag("Patron");

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
        Instantiate(patron, transform.position, transform.rotation);
    }
}
