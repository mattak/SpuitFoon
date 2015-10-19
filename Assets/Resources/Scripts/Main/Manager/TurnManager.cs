using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TurnManager : SingletonMonoBehaviourFast<TurnManager> {
	public int totalTurn = 10;
	public IObservable<int> TurnNumberObservable;
	private int turn;
	private TurnStep turnStep;

	public void Start() {
		StartTurn ();
	}

	public void StartTurn() {
		turn = 0;
		turnStep = TurnStep.Setup;

		NextTurn ();
	}

	public bool NextTurn() {
		Debug.Log ("NextTurn: " + turn);

		if (IsFinishTurn()) {
			Debug.Log ("FIXME: show result");
			GameManager.Instance.GameOver();
			return false;
		}

		turn++;
		turnStep = TurnStep.Cleanup;
		NextTurnStep ();

		return true;
	}

	public void NextTurnStep() {
		Debug.Log ("PreviousTurnStep: " + this.turnStep);
		this.turnStep = this.turnStep.GetNext ();
		Debug.Log ("NextTurnStep: " + this.turnStep);

		switch (this.turnStep) {
		case TurnStep.Setup:
			StartSetupPhase ();
			break;
		case TurnStep.PlayerAction1:
			StartPlayerPhase (Team.Player1, 0);
			break;
		case TurnStep.PlayerAction2:
			StartPlayerPhase (Team.Player2, 1);
			break;
		case TurnStep.Cleanup:
			StartCleanupPhase ();
			break;
		}
	}

	public void StartSetupPhase () {
		PickerManager.Instance.ChangePlayer (Team.Empty);
		this.NextTurnStep ();
	}

	public void StartPlayerPhase (Team team, int order) {
		PickerManager.Instance.ChangePlayer (team);
	}

	public void StartCleanupPhase() {
		PickerManager.Instance.ChangePlayer (Team.Empty);
		this.NextTurn ();
	}

	public int GetRestTurn() {
		return totalTurn - turn;
	}

	public int GetCurrentTurn() {
		return turn;
	}

	public bool IsFinishTurn() {
		return turn >= totalTurn;
	}

	public IObservable<int> GetRestTurnObservable() {
		return Observable
				.EveryUpdate ()
				.Select (_ => this.GetRestTurn())
				.DistinctUntilChanged ();
	}
}