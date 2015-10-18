using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Seat {
	private Team team = Team.Player1;
	private ArrayList circleList = new ArrayList();

	public Seat(Team team) {
		this.team = team;
	}

	public void AddCircle(Circle circle) {
		circleList.Add (circle);
	}

	public ArrayList GetCircleList() {
		return circleList;
	}
}