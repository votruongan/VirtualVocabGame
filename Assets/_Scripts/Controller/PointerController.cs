using UnityEngine;
using System.Collections;

public class PointerController : MonoBehaviour {

	public bool AutoChoose;
	public bool ExistCentralPoint;
	public Vector3 Pos;
	public float WaitSeconds;
	public GameController GC;
	public GameObject LastHitted;
	public GameObject Captured;
	public Ray DebRay;
	public RaycastHit HObject;
	public WaitandChoose WaC;
	public Transform ScreenCenter;
	//public LineRenderer LR;
	Collider Col;


	void Start(){
		if (GameObject.Find ("ScreenCenter") != null) {
			ExistCentralPoint = true;
		} else {
			ExistCentralPoint = false;
		}
	}

	void Update () {

		//----Cast a Ray
		Pos = transform.position;
		DebRay.origin = transform.position;
		DebRay.direction = transform.forward;
		Physics.Raycast (DebRay, out HObject);
		Debug.DrawRay (DebRay.origin, DebRay.direction,Color.magenta);
		Col = HObject.collider;

		//----Process Recieved Information from Ray
		if (Col != null) {
			if (Col.CompareTag ("Untagged")) {
				Captured = null;
				LastHitted = null;
				if (AutoChoose) {
					WaC.Fine = false;
					WaC.Reset ();
				}
			} else {
				Captured = Col.gameObject;
			}
			if (!((Col.CompareTag ("Untagged")) || (Col.CompareTag ("Wall")))) {
				if ((Col.gameObject == LastHitted) && (!(WaC.enabled))) {
					if (AutoChoose) {
						WaC.Tag = Col.tag;
						WaC.Captured = Col.gameObject.name;
						WaC.Fine = true;
						WaC.enabled = true;
					}
					LastHitted = Col.gameObject;
				}
				if ((Col.gameObject != LastHitted)) {
					if (AutoChoose) {
						WaC.Reset ();
					}
					LastHitted = Col.gameObject;		
				} 	
			}
		} else {
			if (AutoChoose) {
				WaC.Reset ();
			}
		}

		//---Update Screen Center Position
		if (ExistCentralPoint) {
			ScreenCenter.position = DebRay.GetPoint (1f);
		}

	}
		
}
