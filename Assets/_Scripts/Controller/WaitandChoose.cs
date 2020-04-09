using UnityEngine;
using System.Collections;

public class WaitandChoose : MonoBehaviour {

	public bool Fine;
	public GameController GC;
	public float WaitSeconds; //Allowed Seconds to for aiming
	public float Elapsed;
	Color  tempCL;
	public SpriteRenderer Progress;
	public string Tag;
	public string Captured;

	void Start(){ 
		tempCL = Progress.color;
		tempCL.a = 0f;
	}

	void FixedUpdate(){
		if (Fine) {
			Elapsed += Time.fixedDeltaTime;		
			tempCL.a = Elapsed / WaitSeconds;
			if (Elapsed >= WaitSeconds) {
				GC.ProcessCaptured ();
				Reset ();
			}
		}
			Progress.color = tempCL;
	}

	public void Reset(){
		Fine = false;
		Captured = "";
		Tag = "";
		Elapsed = 0.0f;
		tempCL.a = 0.0f;
		Progress.color = tempCL;
		gameObject.GetComponent<WaitandChoose>().enabled = false;
	}
}
