using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public bool HideCursor;

	public float Sensitivity = 5.0f;
	public float smoothing = 2.0f;

	Vector2 MouseLook;
	Vector2 SmoothV;

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;

		if (HideCursor) {
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.lockState = CursorLockMode.None;
		}
	}	

	void Update(){
		float SnM = Sensitivity * smoothing;
		Vector2 temp = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		temp = Vector2.Scale (temp, new Vector2 (SnM, SnM));
		SmoothV.x = Mathf.Lerp (SmoothV.x, temp.x, 1f / smoothing);
		SmoothV.y = Mathf.Lerp (SmoothV.y, temp.y, 1f / smoothing);
		MouseLook += SmoothV;
		MouseLook.y = Mathf.Clamp (MouseLook.y, -80f, 80f);
		transform.localRotation = Quaternion.AngleAxis (-MouseLook.y, Vector3.right);
		this.transform.parent.gameObject.transform.localRotation = Quaternion.AngleAxis (MouseLook.x, this.transform.parent.gameObject.transform.up);		

	}

}
