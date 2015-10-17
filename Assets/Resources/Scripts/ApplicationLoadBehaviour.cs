using UnityEngine;
using System.Collections;

public class ApplicationLoadBehaviour : MonoBehaviour {
	public string nextScene = "Main";

	// Use this for initialization
	public void LoadScene () {
		Application.LoadLevel (nextScene);
	}
}