using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;

public class PalletPicker : MonoBehaviour {
	public Team team = Team.Player1;
	public Button[] buttons;
	private string[] placement;

	// Use this for initialization
	void Start () {
		placement = new string[buttons.Length];

		for (int i = 0; i < buttons.Length; i++) {
			int index = i;
			placement[index] = null;

			// button behaves
			buttons[index].OnClickAsObservable().Subscribe(_ => {
				placement[index] = null;
				UpdatePicker();
			});
		}

		UpdatePicker ();
	}

	public void UpdatePicker() {
		System.Random random = new System.Random ();
		for (int i = 0; i < buttons.Length; i++) {
			if (placement[i] == null) {
				Partner partner = PartnerExt.Random ();
				string path = partner.SpritePath (team);
				placement[i] = path;

				Image image = buttons[i].transform.GetChild (0).GetComponent<Image>();
				image.sprite = Resources.Load<Sprite>(path);

				Debug.Log ("pickup: " + partner);
			}
		}
	}
}
