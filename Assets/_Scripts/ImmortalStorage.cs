using UnityEngine;
using System.Collections;

public class ImmortalStorage : MonoBehaviour {

	public bool AllowMouseClick;
	public bool AutoChoose;
	public bool EnableVRMode;
	public int LanguageIndex;
	public Color TextColor;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
