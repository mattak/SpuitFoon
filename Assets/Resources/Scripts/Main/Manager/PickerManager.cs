﻿using UnityEngine;
using System.Collections;

public class PickerManager : SingletonMonoBehaviourFast<PickerManager> {
	private Team targetTeam;
	public Partner? selectedPartner;
	public GameObject selectedPartnerObject;

	public GameObject pickupTable1;
	public GameObject pickupTable2;

	// Use this for initialization
	void Start () {
		targetTeam = Team.Player1;
		selectedPartner = null;
		ChangePlayer (Team.Empty);
	}

	public void PickupPartner(Partner partner, GameObject partnerObject, Team team, Vector3 position) {
		Debug.Assert (team == targetTeam);
		this.selectedPartner = partner;
		this.selectedPartnerObject = partnerObject; // (GameObject)Instantiate(partner.LoadPartner (team), position, Quaternion.identity);
		this.selectedPartnerObject.transform.position = position;
	}

	public bool PutdownPartner(Vector2 position) {
		if (this.targetTeam == Team.Empty || this.selectedPartner == null) {
			return false;
		}

		Partner partner = (Partner)this.selectedPartner;

		if (AreaBoardManager.Instance.PutPartner (partner, this.targetTeam, position)) {
			this.selectedPartner = null;
			this.selectedPartnerObject = null;
			return true;
		} else {
			Debug.Log ("PutPartner failure");
		}

		return false;
	}

	public bool CanPutdownPartner() {
		return (this.targetTeam != Team.Empty && this.selectedPartner != null);
	}

	public bool CanPickupPartner() {
		return (this.selectedPartner == null);
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