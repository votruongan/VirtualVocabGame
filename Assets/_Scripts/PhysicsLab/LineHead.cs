using UnityEngine;
using System.Collections;

public class LineHead : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Charge")){
			switch (other.transform.parent.name){
				case "Volmeter":
					
				break;
			}
		}
	}
}
