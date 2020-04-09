using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public bool IsInreciever;
	public GameController GC;

	void OnTriggerEnter(Collider Recieved){
		if (Recieved.CompareTag ("Pointer")) {
			if (IsInreciever) {
				GC.ToggleMenu ();
			}
		}
	}

	void OnTriggerExit(Collider Recieved){
		if (Recieved.CompareTag ("Pointer")) {
			if (!(IsInreciever)) {
				GC.ToggleMenu ();
			}
		}
	}
}
