using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviourFast<ScoreManager> {
	public Text scoreText1;
	public Text scoreText2;

	// Use this for initialization
	void Start () {
	}

	public void ApplyResult() {
		float score1 = AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player1);
		float score2 = AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player2);
		scoreText1.text = string.Format("{0:N1}", score1 * 100f);
		scoreText2.text = string.Format("{0:N1}", score2 * 100f);
	}
}