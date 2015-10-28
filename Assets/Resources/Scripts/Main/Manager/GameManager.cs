using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	public void Start() {

	}

	public void GameStart() {
	}

	public void GameOver() {
		float score1 = AreaBoard.Instance.CalcurateTeamAreaPercentage (Team.Player1);
		float score2 = AreaBoard.Instance.CalcurateTeamAreaPercentage (Team.Player2);
	}
}