using UnityEngine;
using System.Collections;

public class Seat {
	private int order;
	private Team team = Team.Player1;
	private ArrayList circleList = new ArrayList();

	public Seat(int order, Team team) {
		this.order = order;
		this.team = team;
	}

	public void AddCircle(Circle circle) {
		circleList.Add (circle);
	}

	public IList GetCircleList() {
		return circleList;
	}
}