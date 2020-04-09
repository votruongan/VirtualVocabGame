using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMoves : MonoBehaviour {
	public float speed;
	public Transform ThisTrf, Head;
	public Vector2 NextFrontStep;
	public float YRotat;
	void Start () {
		ThisTrf = this.GetComponent<Transform>();
		NextFrontStep = new Vector2 (0.0F,speed);
	}

	void ApplyTransform(Vector2 Offset){
		Vector3 tmp = ThisTrf.position;
		tmp.x += Offset.x;
		tmp.z += Offset.y;
		ThisTrf.position = tmp;
	}

	void FixedUpdate () {
		YRotat = Head.eulerAngles.y * 3.141593f / 180; //Convert from Degree to Radian
		NextFrontStep.y = Mathf.Cos (YRotat) * speed;
		NextFrontStep.x = Mathf.Sin (YRotat) * speed;

		Debug.DrawRay (ThisTrf.position, new Vector3 (ThisTrf.position.x + NextFrontStep.x, 2.0f, ThisTrf.position.z + NextFrontStep.y),Color.yellow);

		if (Input.GetAxis ("Vertical") > 0.0f) {
			ApplyTransform (NextFrontStep);
		}
		if (Input.GetAxis ("Vertical") < 0.0f) {
			ApplyTransform (new Vector2 (-NextFrontStep.x, -NextFrontStep.y));
		}
		if (Input.GetAxis ("Horizontal") > 0.0f) {
			ApplyTransform (new Vector2 (-NextFrontStep.x, NextFrontStep.y));
		}
		if (Input.GetAxis ("Horizontal") < 0.0f) {
			ApplyTransform (new Vector2 (NextFrontStep.x, -NextFrontStep.y));
		}
	}
}
