using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour {

	public static double hour = 10;
	public static double minute = 00;
	//public static double second = 00;

	public static double lastChange = 0;

	void Update () 
	{
		if (minute >= 0 && hour >= 0) 
		{
			if (Time.time - lastChange > 1.0) 
			{
				if (minute == 0) 
				{
					minute = 59;
					hour--;
				}
				minute--;
				lastChange = Time.time;
			}
		}
	}
}
