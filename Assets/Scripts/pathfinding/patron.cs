using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class patron : MonoBehaviour {

	public List<Vector2> WPoints = new List<Vector2>();
	private PolyNavAgent _agent;


	public PolyNavAgent agent{
		get
		{
			if (_agent == null)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}
	private bool satisfied;
	//private 
	void Start () {

		int pos = GameManager.resourceTable[1].availablePosition();
		if (pos < 0) {
			//no chair available dont walk to table. 
		} else {
			Transform chair =  GameManager.resourceTable [1].getPosition(pos);
			GameManager.resourceTable [1].swapAvailable (pos);
			agent.SetDestination (chair.position);


		}
	}

	// Update is called once per frame
	void Update () {

	}

}
