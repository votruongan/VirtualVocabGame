using UnityEngine;
using System.Collections;

public class IObjectsInScene : MonoBehaviour {

	public GameController GC;

	public int SpawnNumber;
	public int PendingNumber;
	public int IOLength;
	public int FoundNumber = 0;

	public GameObject[] SpawnableObjects;
	public GameObject[] AvailableObjects;
	public GameObject[] InteractableObjects;
	public Transform[] SpawnPosition;
	public string[] FindingObjects;
	public string[] PendingObjects;	
	public int[] FindingIOPosition;
	public int[] PendingIOPosition;


	void Start () {
		GameObject[] TempGOL = AvailableObjects;
		for (int i = 0; i < TempGOL.Length; i++) {
			int TempInt = Random.Range(0,(TempGOL.Length - 1));
			GameObject Tempp = TempGOL [TempInt];
			TempGOL [TempInt] = TempGOL[i];
			TempGOL[i] = Tempp;
		}

		for (int i = 0; i < SpawnNumber; i ++) {
			GameObject TempObj = TempGOL[i];
			Instantiate (TempObj,SpawnPosition[i].position,TempObj.transform.rotation);
			GameObject NewMade = GameObject.Find (TempObj.name + "(Clone)");
			NewMade.name = TempObj.name;
		}
		InteractableObjects = GameObject.FindGameObjectsWithTag ("Objects");

		IOLength = InteractableObjects.Length;  //InteractableObjects Length
		PendingNumber = IOLength;

		PendingObjects = new string[IOLength];
		PendingIOPosition = new int[IOLength];

		for (int i = 0; i < IOLength; i ++){
			PendingObjects [i] = InteractableObjects [i].name;
			PendingIOPosition [i] = i;
		}

		//---Create Pending List
		//-------Shuffle Positions
		for (int i = 0; i < IOLength; i++) {

			int TempIndex = Random.Range (0,IOLength - 1);
			if (TempIndex != i){
				string TempString = PendingObjects[i];
				PendingObjects[i] = PendingObjects[TempIndex];
				PendingObjects[TempIndex] = TempString;
				int TempInt = PendingIOPosition [i];
				PendingIOPosition [i] = PendingIOPosition [TempIndex];
				PendingIOPosition [TempIndex] = TempInt;
			}
		}
		//------Assign Pending List to Finding List
		for (int i = 0; i < FindingObjects.Length; i++) {
			PendingNumber--;
			UpdateLists (i);
			Debug.Log ("Assigned Obj " + i +" : "+ FindingObjects[i]);
		}

		foreach (GameObject DebIO in InteractableObjects) {
			Debug.Log(DebIO.name);
		}

		GC.InteractableObjects = InteractableObjects;
		GC.PendingObjects = PendingObjects;
		GC.FindingObjects = FindingObjects;
		GC.FindingIOPosition = FindingIOPosition;
		GC.PendingIOPosition = PendingIOPosition;
		GC.GetIObjects ();
	} 
	void UpdateLists(int numb){
		FindingIOPosition [numb] = PendingIOPosition [0];
		FindingObjects [numb] = PendingObjects [0];
		for (int j = 0; j < PendingNumber; j++) {
			PendingObjects [j] = PendingObjects [j + 1];				
			PendingIOPosition [j] = PendingIOPosition [j + 1];
		}
		PendingObjects [PendingNumber] = "";
		PendingIOPosition [PendingNumber] = -1;
	}
}
