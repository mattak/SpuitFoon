using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TouchSpawnBehaviour : MonoBehaviour {
	public Button button;

	// Use this for initialization
	void Start () {
		button.OnClickAsObservable ()
			.Subscribe (thisUnit => {
				Debug.Log ("Clicked");
			});
	}
}