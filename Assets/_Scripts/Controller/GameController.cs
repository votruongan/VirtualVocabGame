using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool VROn = false;

	public bool AllowMClick = true;
	public GameObject FOList;
	public GameObject Menu;
	public GameObject CamLeft;
	public GameObject CamRight;
	public string NextScene;
	public GameObject NextSceneButt;
	public GameObject UIInSensor;
	public GameObject UIOutSensor;

	public Sprite GreenCircle;
	public Sprite RedCircle;
	public SpriteRenderer IndicatorCircle;

	public IObjectsInScene IOController;
	public ImmortalStorage Storage;
	public SceneController SceneControl;
	public PointerController Pointer;
	public GvrViewer GvrMain;

	public int PendingNumber;
	public int FoundNumber = 0;
	public int IOLength;
	public GameObject[] InteractableObjects;
	public string[] FindingObjects;
	public string[] PendingObjects;	
	public int[] FindingIOPosition;
	public int[] PendingIOPosition;
	public TextMesh[] FObjsOutput;
	public TextController[] TextMeshesList;
	public Transform[] SpawnPosition;

	public ButtonController LastButt;
	public TextMesh ObjectIndicator;
	public TextMesh Notification;

	void Start () {

		//---Get Text Meshes List
		if (TextMeshesList.Length == 0) {
			GameObject[] TempGOL;
			int Index = 0;
			TempGOL = GameObject.FindGameObjectsWithTag ("Text");
			TextMeshesList = new TextController[TempGOL.Length];
			foreach (GameObject TempGO in TempGOL) {
				TextMeshesList [Index] = TempGO.GetComponent<TextController> ();
				Index++;
			}
		}
		//CamLeft = GameObject.Find ("Main Camera Left");
		//CamRight = GameObject.Find ("Main Camera Right");

		if (SceneManager.GetActiveScene ().name == "Main_1") {
			SceneControl.EnglishBlock.SetActive (false);
			SceneControl.PhysicsBlock.SetActive (false);
		}

		//--- Intialize Scene
		Storage = GameObject.Find ("Storage").GetComponent<ImmortalStorage>();
		GvrMain.VRModeEnabled = Storage.EnableVRMode;
		foreach (TextController TC in TextMeshesList) {
			TC.GetDisplayText ();
			TC.DisplayText.color = Storage.TextColor;
		}
		Pointer.AutoChoose = Storage.AutoChoose;
		AllowMClick = Storage.AllowMouseClick;


		UpdateLanguage ();
		if (Menu != null)
			Menu.SetActive (false);
	}


	void Update(){
		GameObject CGO = Pointer.Captured;
		if (CGO != null) {
			if ((CGO.CompareTag("UI"))||(CGO.CompareTag("SubjectSelect"))||(CGO.CompareTag("SceneSelect"))) {
				if (LastButt != null)
					LastButt.UnHightLight ();
				if (CGO.GetComponent<ButtonController>() != null) {
					CGO.GetComponent<ButtonController> ().HightLight ();
					LastButt = CGO.GetComponent<ButtonController> ();
				}
			}
		}
		if ((Input.GetMouseButtonDown(0))&&(CGO != null)&&(AllowMClick)){
			Debug.Log("MB0-Down-" + CGO.name);
			ProcessCaptured ();
			}
		}

	public void ProcessCaptured(){
		string CName = Pointer.Captured.name;
		switch(Pointer.Captured.tag){
		case "UI":
			GotUIButton (CName);
			break;
		case "SceneSelect":
			SceneControl.RequestScene (CName);
			break;
		case "SubjectSelect":
			SceneControl.RequestSubject (CName);
			UpdateLanguage ();
			break;
		case "Objects":	
			FoundObject (Pointer.Captured);
			break;
		case "Draggable":
			//ObjectController CObjct = Pointer.Captured.GetComponent<ObjectController> ();
			//CObjct.GotDrag = !CObjct.GotDrag;
			break;
		case "SpawnObject":
			Pointer.Captured.GetComponent<ButtonController>().Spawn(SpawnPosition[0]);
			break;
		}
	}

	//-----Controller for buttons
	void GotUIButton(string name){
		switch (name) {

		//---Back Button
		case "English_Back":
			SceneControl.EnglishBlock.SetActive (false);
			SceneControl.RequestSubject ("MainMenu");
			break;
		case "Physics_Back":
			SceneControl.PhysicsBlock.SetActive (false);
			SceneControl.RequestSubject ("MainMenu");
			break;
		case "ToggleVR": 
			GvrMain.VRModeEnabled = !GvrMain.VRModeEnabled;
			Storage.EnableVRMode = GvrMain.VRModeEnabled;
			break;
		case "Quit":
			Application.Quit ();
			break;
		case "MainMenu":
			SceneManager.LoadScene("Main_1");
			break;
		case "NextScene":
			SceneManager.LoadScene(NextScene);
			break;

		//-----Language Controller 0-English, 1-Vietnamese
		case "English_Lang":
			Storage.LanguageIndex = 0;
			UpdateLanguage ();
			break;
		case "Vietnamese_Lang":
			Storage.LanguageIndex = 1;
			UpdateLanguage ();
			break;
		}
	}


	void UpdateLanguage(){
		foreach (TextController TC in TextMeshesList) {
			TC.UpdateText (Storage.LanguageIndex);
		}
	}

	//-----Update Finding Objects List, Pending Objects List and Finding Objects Position in Main Objects List
	void UpdateLists(int numb){
		FindingIOPosition [numb] = PendingIOPosition [0];
		FindingObjects [numb] = PendingObjects [0];
		FObjsOutput[numb].text = PendingObjects [0];
		for (int j = 0; j < PendingNumber; j++) {
			PendingObjects [j] = PendingObjects [j + 1];				
			PendingIOPosition [j] = PendingIOPosition [j + 1];
		}
		PendingObjects [PendingNumber] = "";
		PendingIOPosition [PendingNumber] = -1;
	}

	public void ToggleMenu(){
		if (FOList != null){
			ToggleObject(FOList);
		}
		ToggleObject (Menu);
		ToggleObject (UIOutSensor);
		ToggleObject (UIInSensor);
	}

	public void ToggleObject(GameObject Target){
		Target.SetActive (!Target.activeInHierarchy);
	}


	public void GetIObjects(){
		


		for (int i = 0; i < FindingObjects.Length; i++) {
			FObjsOutput [i].text = FindingObjects [i];
		}
	}


	void FoundObject(GameObject CapturedObject){ //For English only
		
		int Index = 0;
		bool Match = false;

		for (int i = 0; i < FindingObjects.Length; i++) {
			if (FindingObjects [i] == CapturedObject.name) {
				Index = i;
				Match = true;
			}
		}

		if (Match) {
			Debug.Log ("***** Object: " + CapturedObject.name + " Matched");
			FoundNumber++;
			PendingNumber--;
			if (PendingObjects [0] != "") {
				UpdateLists (Index);
			} else {
				FObjsOutput [Index].text = "";
				FindingObjects [Index] = "";
			}
			UpdateICircle (GreenCircle);
			Debug.Log (ObjectIndicator.name);
			ObjectIndicator.transform.parent.gameObject.SetActive (true);
			ObjectIndicator.text = CapturedObject.name + " " + CapturedObject.GetComponent<TextContainer> ().InsideText;
			CapturedObject.GetComponent<AudioSource> ().Play ();
		} else {
			ObjectIndicator.gameObject.SetActive (false);
			UpdateICircle (RedCircle);
		}

		if (FoundNumber == InteractableObjects.Length) {
			Notification.text = "YOU WIN";
			Notification.gameObject.SetActive (true);
			NextSceneButt.SetActive (true);
		}
	}			

	void UpdateICircle(Sprite UpSprite){
		IndicatorCircle.gameObject.SetActive (false);
		IndicatorCircle.gameObject.SetActive (true);
		IndicatorCircle.sprite = UpSprite;
	}
}
