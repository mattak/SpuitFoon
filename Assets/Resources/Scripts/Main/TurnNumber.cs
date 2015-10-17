using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TurnNumber : MonoBehaviour {
	public Text turnText;

	void Start () {
		TurnManager.Instance.GetRestTurnObservable ().Subscribe (turn => {
			this.turnText.text = turn.ToString ();
		});
	}
}
