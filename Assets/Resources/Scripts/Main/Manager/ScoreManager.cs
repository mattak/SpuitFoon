using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviourFast<ScoreManager> {
	public Text scoreText1;
	public Text scoreText2;
	public Image resultImage1;
	public Image resultImage2;

	// Use this for initialization
	void Start () {
	}

	public void ApplyResult() {
		float score1 = AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player1);
		float score2 = AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player2);
		scoreText1.text = string.Format("{0:N1}", score1 * 100f);
		scoreText2.text = string.Format("{0:N1}", score2 * 100f);

		if (score1 > score2) {
			this.SetWinLose(resultImage1, resultImage2);
		} else if (score1 < score2) {
			this.SetWinLose(resultImage2, resultImage1);
		} else {
			this.SetLose (resultImage1);
			this.SetLose (resultImage2);
		}
	}

	private void SetWinLose(Image imageWinner, Image imageLoser) {
		imageWinner.sprite = Resources.Load<Sprite> ("Sprites/word_win");
		this.SetLose(imageLoser);
	}

	private void SetLose(Image imageLoser) {
		imageLoser.sprite = Resources.Load<Sprite> ("Sprites/word_lose");
	}
}