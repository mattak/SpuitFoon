using UnityEngine;
using System.Collections;

public class PickerManager : SingletonMonoBehaviourFast<PickerManager> {
	private Team targetTeam;
	private Partner? selectedPartner;

	public GameObject pickupTable1;
	public GameObject pickupTable2;

	// Use this for initialization
	void Start () {
		targetTeam = Team.Player1;
		selectedPartner = null;
		ChangePlayer (Team.Empty);
	}

	public void PickupPartner(Partner partner, Team team) {
		Debug.Assert (team == targetTeam);
		this.selectedPartner = partner;
	}

	public void PutdownPartner(Vector2 position) {
		if (this.targetTeam == Team.Empty || this.selectedPartner == null) {
			return;
		}

		Partner partner = (Partner)this.selectedPartner;

		if (AreaBoard.Instance.PutPartner(partner, this.targetTeam, position)) {
			Debug.Log ("Putted");
		}
	}

	public bool CanPutdownPartner() {
		return (this.targetTeam != Team.Empty && this.selectedPartner != null);
	}

	public void ChangePlayer(Team team) {
		this.targetTeam = team;

		if (team == Team.Player1) {
			pickupTable1.SetActive (true);
			pickupTable2.SetActive (false);
		} else if (team == Team.Player2) {
			pickupTable1.SetActive (false);
			pickupTable2.SetActive (true);
		} else {
			pickupTable1.SetActive (false);
			pickupTable2.SetActive (false);
		}

		selectedPartner = null;
	}
}