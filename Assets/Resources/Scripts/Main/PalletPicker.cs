using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;

public class PalletPicker : MonoBehaviour {
	public Team team = Team.Player1;
	// public Button[] buttons;
	public Collider2D[] placeHolders;
	public GameObject stageObject;
	private Partner?[] placement;

	// Use this for initialization
	void Start () {
		placement = new Partner?[placeHolders.Length];

		for (int i = 0; i < placeHolders.Length; i++) {
			int index = i;
			placement[index] = null;

			// mouseDown FIXME: change condition to sprite collision
			this.UpdateAsObservable ()
				.Where (_ => Input.GetMouseButtonDown (0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (point => Physics2D.OverlapPoint (point))
				.Where (collider => collider == placeHolders[index])
				.Where (_ => PickerManager.Instance.CanPickupPartner())
				.Where (_ => placement[index] != null)
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, stageObject.transform.position.z - 1))
				.Subscribe(position => {
						Partner partner = (Partner)placement[index];
						PickerManager.Instance.PickupPartner (partner, team, position);
				});
		}

		// mouse move
		this.UpdateAsObservable ()
				.Where (_ => PickerManager.Instance.CanPutdownPartner())
				.Where (_ => !Input.GetMouseButtonDown (0) && !Input.GetMouseButtonUp (0) && Input.GetMouseButton (0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, stageObject.transform.position.z - 1))
				.Subscribe (position => {
					PickerManager.Instance.selectedPartnerObject.transform.position = position;
				});

		// mouseUp
		this.UpdateAsObservable ()
				.Where (_ => Input.GetMouseButtonUp (0))
				.Where (_ => PickerManager.Instance.CanPutdownPartner())
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, stageObject.transform.position.z - 1)) // TODO: summerize
				.Subscribe(position => {
					if (PickerManager.Instance.PutdownPartner (new Vector2(position.x, position.y))) {
						// Put down.
					}
					else {
						// place back original position.
					}
				});

		UpdatePicker ();
	}

	public void UpdatePicker() {
		System.Random random = new System.Random ();

		for (int i = 0; i < placeHolders.Length; i++) {
			if (placement[i] == null) {
				// FIXME
				Partner partner = PartnerExt.Random ();
				Debug.Log("partner: " + partner);
				string path = partner.SpritePath(team);
				placement[i] = partner;
			}
		}
	}
}
