using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour {
	
	public GameObject SettingsBlock;
	public GameObject EnglishBlock;
	public GameObject PhysicsBlock;
	public GameObject MainMenu;

	public TextMesh CurrentSubject;

	public void RequestScene(string name){
		SceneManager.UnloadScene (0);
		SceneManager.UnloadScene (1);
		SceneManager.UnloadScene (2);
		SceneManager.LoadScene (name); 
	}

	public void RequestSubject (string name){
		SettingsBlock.SetActive (false);
		MainMenu.SetActive (false);
		switch (name) {
		case "English": 
			EnglishBlock.SetActive (true);
			CurrentSubject.text = name;
			break;
		case "Physics":
			PhysicsBlock.SetActive (true);
			CurrentSubject.text = name;
			break;
		case "MainMenu":
			MainMenu.SetActive (true);
			SettingsBlock.SetActive (true);
			break;
		}
	}
}
