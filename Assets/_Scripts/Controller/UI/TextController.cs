using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

	public string[] AltText;
	public TextMesh DisplayText;

	public void GetDisplayText () {
		DisplayText = this.gameObject.GetComponent<TextMesh> ();
	}

	public void UpdateText (int Index) {
		if (DisplayText != null){
			Debug.Log ("Change: " + gameObject.name + " -> " + AltText [Index]);
			DisplayText.text = AltText [Index];
		}
	}
}
