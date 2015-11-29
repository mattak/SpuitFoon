using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using ViewModel;

/**
 * Pallet Picker.
 * Controls flow of pallect behaviour. pickup & put partner.
 */
namespace SpuitFoon
{
	public class PalletPicker : MonoBehaviour
	{
		public Team team = Team.Player1;
		public Collider2D[] placeHolders;
		public GameObject stageObject;
		private Partner?[] placement;
		private GameObject[] placementObject;

		// Use this for initialization
		void Start ()
		{
			placement = new Partner?[placeHolders.Length];
			placementObject = new GameObject[placeHolders.Length];

			for (int i = 0; i < placeHolders.Length; i++) {
				int index = i;
				placement [index] = null;

				// mouseDown FIXME: change condition to sprite collision
				this.OnMouseDownCollider2DAsObservale ()
					.Do (collider => Debug.Log ("collider: " + collider))
					.Where (collider => collider == placeHolders [index] && PickerManager.Instance.CanPickupPartner () && placement [index] != null)
					.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
					.Select (p => new Vector3 (p.x, p.y, stageObject.transform.position.z - 1))
					.Subscribe (position => {
					Partner partner = (Partner)placement [index];
					GameObject partnerObject = (GameObject)placementObject [index];
					partnerObject.transform.parent = null;
					PickerManager.Instance.PickupPartner (partner, partnerObject, team, position);

					placement [index] = null;
					placementObject [index] = null;
				});
			}

			// mouse move
			this.OnMouseMoveWorldPointAsObservable ()
				.Select (p => new Vector3 (p.x, p.y, stageObject.transform.position.z - 1))
				.Subscribe (position => {
				PickerManager.Instance.selectedPartnerObject.transform.position = position;
			});

			// mouseUp
			this.OnMouseUpWorldPointAsObservable ()
				.Select (p => new Vector3 (p.x, p.y, stageObject.transform.position.z - 1)) // TODO: summerize
				.Subscribe (position => {
				if (PickerManager.Instance.PutdownPartner (new Vector2 (position.x, position.y))) {
					UpdatePicker ();
				} else {
					// place back original position.
				}
			});

			UpdatePicker ();
		}

		public void UpdatePicker ()
		{
			System.Random random = new System.Random ();

			for (int i = 0; i < placeHolders.Length; i++) {
				if (placement [i] == null) {
					Partner partner = PartnerExt.Random ();
					string path = partner.SpritePath (team);
					Vector3 holderPosition = placeHolders [i].transform.position; 
					Vector3 position = new Vector3 (holderPosition.x, holderPosition.y, stageObject.transform.position.z);
					placement [i] = partner;
					placementObject [i] = (GameObject)Instantiate (partner.LoadPartner (team), position, Quaternion.identity);
				}
			}
		}
	}
}