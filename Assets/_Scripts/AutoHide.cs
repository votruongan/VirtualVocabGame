using UnityEngine;
using System.Collections;

public class AutoHide : MonoBehaviour {

	public float HideinSecs;
	public GameController GC;

	void OnEnable () {
		StartCoroutine(WaitandHide(HideinSecs));
	}

	IEnumerator WaitandHide(float Index){
		yield return new WaitForSeconds (Index);
		this.gameObject.SetActive (false);
	}
}
