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

	public void Draw(Team team, float positionZ) {
		string path = team.PrefabPath();

		foreach (Circle circle in circleList) {
			Vector3 position = new Vector3(circle.point.x, circle.point.y, positionZ);
			GameObject circleObject = Resources.Load<GameObject> (path);
			SpriteRenderer circleRenderer = circleObject.GetComponent<SpriteRenderer>();
			float originalScale = circleRenderer.bounds.size.x / 2 / circle.radius;
			float scale = 1.0f / originalScale;
			GameObject instanceObject = GameObject.Instantiate(circleObject, position, Quaternion.identity) as GameObject;
			instanceObject.transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}