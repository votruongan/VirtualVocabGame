using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

	public Rigidbody GORigibody;
	public Transform Sensor;
	public Transform ScreenCenter;

	public float AdditionalYOffset;
	public float offset;
	public bool ReadyDrag;
	public bool GotDrag;

	public Vector3 LastPos;
	public Vector3 CurrentPos;
	public Vector3 TempPos;

	void Start(){
		Debug.Log ("Start Searching...");
		Sensor = GameObject.Find ("Sensor").transform;
		Debug.Log ("Done with Sensor: " + Sensor );
		ScreenCenter = GameObject.Find ("Screen_Center").transform;
		Debug.Log ("Done with SC: " + ScreenCenter);
		GORigibody = gameObject.GetComponent<Rigidbody> ();

	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Pointer")) {
			ReadyDrag = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Pointer")) {
			ReadyDrag = false;
		}
	}

	void OnMouseDown(){
		if (ReadyDrag) {
			GotDrag = true;
			LastPos = Sensor.position;
			GORigibody.isKinematic = true;

		}
	}

	void OnMouseUp(){
		GotDrag = false;
		GORigibody.isKinematic = false;
	}

	void Update(){
		
		if ((GORigibody.transform.position.y > 0.1f)&&(!(GotDrag))) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}


		if (GORigibody.velocity.y == 0.0f) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		}

		if (GotDrag) {
			/*CurrentPos = Sensor.position;
			TempPos = new Vector3 (CurrentPos.x - LastPos.x, CurrentPos.y - LastPos.y, CurrentPos.z - LastPos.z);
			transform.position += TempPos/offset;
			if (AdditionalYOffset != 0) {
				transform.position = new Vector3 (transform.position.x, transform.position.y - (TempPos.y / AdditionalYOffset), transform.position.z);
			}
			TempPos = Vector3.zero;
			LastPos = CurrentPos;
			gameObject.GetComponent<Rigidbody>().Sleep();
			*/
			transform.position = ScreenCenter.position;


			if (AdditionalYOffset != 0) {
				transform.position = new Vector3 (transform.position.x, transform.position.y - (TempPos.y / AdditionalYOffset), transform.position.z);
			}
		}
	}

}
