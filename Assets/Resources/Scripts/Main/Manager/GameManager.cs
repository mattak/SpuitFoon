using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	public void Start() {
		GameStart ();
	}

	public void GameStart() {
		LayerManager.Instance.Layer.SetValueAndForceNotify(GamePhase.Start);
		Invoke ("GamePlay", 3);
	}

	public void GamePlay() {
		LayerManager.Instance.Layer.SetValueAndForceNotify(GamePhase.Playing);
		TurnManager.Instance.StartTurn ();
	}

	public void GameOver() {
		ScoreManager.Instance.ApplyResult ();
		LayerManager.Instance.Layer.SetValueAndForceNotify(GamePhase.Result);
	}
}