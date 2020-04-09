using UnityEngine;
using System.Collections;

public enum WhattoSense
{
	UI,UI_Out,SpawnObject
}

public class Sensor : MonoBehaviour {

	public GameController GC;
	public WhattoSense SensorType;
	public GameObject SOCPanel;

	void Start(){
		if (GC == null)
			GC = GameObject.Find ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider Recieved)
	{
		if (Recieved.CompareTag ("Pointer")) {
		switch (SensorType){
			case WhattoSense.UI:
				if (Recieved.CompareTag ("Pointer")) {
					GC.ToggleMenu ();
				}
				break;
			case WhattoSense.SpawnObject:
					GC.ToggleMenu ();
				break;
			}
		}
	}


	void OnTriggerExit(Collider Recieved)
	{
		if (Recieved.CompareTag ("Pointer")) {
		switch (SensorType){
			case WhattoSense.UI_Out:
					GC.ToggleMenu ();
				break;

			case WhattoSense.SpawnObject:
				GC.ToggleObject (SOCPanel);
				break;
			}
		}
	}
}
