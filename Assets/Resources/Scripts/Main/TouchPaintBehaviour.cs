using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TouchPaintBehaviour : MonoBehaviour {
	public GameObject parentObject;

	// Use this for initialization
	void Start () {
		/*
		var updateObservable = Observable.EveryUpdate ();
		var mouseDown = updateObservable.Where (_ => Input.GetMouseButtonDown (0));
		var mouseMove = updateObservable.Where (_ => !Input.GetMouseButtonDown (0) && !Input.GetMouseButtonUp (0) && Input.GetMouseButton (0));
		var mouseUp = updateObservable.Where (_ => Input.GetMouseButtonUp (0));

		mouseDown.Subscribe (_ => {
			Debug.Log ("down");
			// PickerManager.Instance.PickupPartner(team, partner);
			// Instantiate SpriteObject
			// Remove Image Content
		});

		mouseMove
				.Where(_ => PickerManager.Instance.CanPutdownPartner())
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Subscribe (position => {
					// GameObject partner;
					// partner.transform.position = position;
				});
		mouseUp
				.Where (_ => true) // TODO check partner is not missing
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Subscribe (_ => {
				});
		*/

		/*
		mouseDown.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, parentObject.transform.position.z))
				.Subscribe (position => {
					Debug.Log ("pressed: " + position);
					// TODO: export logic
					if (TurnManager.Instance.IsFinishTurn()) {
						Debug.Log ("team1: " + AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player1));
						Debug.Log ("team2: " + AreaBoard.Instance.CalcurateTeamAreaPercentage(Team.Player2));
					}
					else if (PickerManager.Instance.CanPutdownPartner()) {
						PickerManager.Instance.PutdownPartner(position);
					}
				});
				*/
	}
}