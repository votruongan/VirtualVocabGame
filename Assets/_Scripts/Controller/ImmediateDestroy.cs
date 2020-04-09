using UnityEngine;
using System.Collections;

public class ImmediateDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider Recieved){
		GameObject.Destroy (Recieved.gameObject);
	}
}
