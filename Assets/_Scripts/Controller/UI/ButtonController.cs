using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	public Material HL; //Hightlight Material
	public Material NM; //Normal Material
	public Object WillSpawnObject;

	public void UnHightLight(){
		gameObject.GetComponent<Renderer>().material = NM;
	}

	public void HightLight(){
		gameObject.GetComponent<Renderer>().material = HL;
	}

	public void Spawn(Transform Pos){
		GameObject.Instantiate(WillSpawnObject,Pos.position,Pos.rotation);
		Debug.Log ("Spawned: " + WillSpawnObject.name + " at " + Pos.position + " rotate: " +Pos.rotation);
	}
}
