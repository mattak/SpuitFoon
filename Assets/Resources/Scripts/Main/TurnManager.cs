using UnityEngine;
using System.Collections;

public class TurnManager : SingletonMonoBehaviourFast<TurnManager> {
	public int totalTurn = 10;
	private int turn;
	private ArrayList turnTeams;

	public void Start() {
		StartTurn ();
	}

	public void StartTurn() {
		turn = 0;
		turnTeams = new ArrayList ();

		for (int i = 0; i < totalTurn; i++) {
			if (i % 2 == 0) {
				turnTeams.Add (Team.Player1);
			}
			else {
				turnTeams.Add (Team.Player2);
			}
		}
	}

	public bool NextTurn() {
		turn++;
		return IsFinishTurn ();
	}

	public int GetRestTurn() {
		return totalTurn - turn;
	}

	public Team GetTurnTeam() {
		return (Team)turnTeams[turn];
	}

	public int GetCurrentTurn() {
		return turn;
	}

	public bool IsFinishTurn() {
		return turn >= totalTurn;
	}
}