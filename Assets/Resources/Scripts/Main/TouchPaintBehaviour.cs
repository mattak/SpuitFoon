using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TouchPaintBehaviour : MonoBehaviour {
	public GameObject parentObject;

	// Use this for initialization
	void Start () {
		Observable
				.EveryUpdate()
				.Where (_ => Input.GetMouseButtonDown(0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, parentObject.transform.position.z))
				.Subscribe (position => {
					// TODO: export logic
					if (TurnManager.Instance.IsFinishTurn()) {
						Debug.Log ("team1: " + AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player1));
						Debug.Log ("team2: " + AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player2));
					}
					else {
						// area board update
						Painter painter = new FudoPainter(new Vector2(position.x, position.y), 1.0f);
						Team team = TurnManager.Instance.GetTurnTeam();
						int turn = TurnManager.Instance.GetCurrentTurn();
						Seat seat = new Seat(turn, team);
						painter.Paint (seat);
						AreaBoard.Instance.AddSeat (seat, team);

						// draw update
						string path = team.PrefabPath();
						GameObject stamp = Instantiate(Resources.Load (path), position, Quaternion.identity) as GameObject;
						stamp.transform.parent = parentObject.transform;

						// goto next turn
						TurnManager.Instance.NextTurn();
					}
				});
	}
}