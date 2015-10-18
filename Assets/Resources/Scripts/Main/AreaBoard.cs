using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaBoard : SingletonMonoBehaviourFast<AreaBoard> {
	private Dictionary<Vector2, Team> areaTable; // Vector2, Team
	private Dictionary<int, Seat> seatStack; // Int, Seat
	private int seatOrder;
	public int areaDivision = 200;
	public GameObject areaObject;

	public void Start() {
		areaTable = new Dictionary<Vector2, Team> ();
		seatStack = new Dictionary<int, Seat> ();
		seatOrder = 0;

		float areaRadius = this.CalculateAreaRadius (areaObject);
		float areaStep = this.CalculateAreaStep (areaObject, areaDivision);
		InitializeArea (areaObject.transform.position, areaRadius, areaStep);
		InitializeSeatStack ();
	}

	// TODO: change flexibility for 
	public void InitializeArea(Vector2 centerPoint, float radius, float step) {
		areaTable.Clear ();
		float sx = centerPoint.x - radius;
		float sy = centerPoint.y - radius;
		float ex = centerPoint.x + radius;
		float ey = centerPoint.y + radius;

		for (float y = sy; y <= ey; y += step) {
			for (float x = sx; x <= ex; x += step) {
				Vector2 point = new Vector2 (x, y);
				if (Vector2.Distance (centerPoint, point) <= radius) {
					areaTable[point] = Team.Empty;
				}
			}
		}

		// put first area
		Painter painter1 = new FudoPainter(new Vector2(sx, centerPoint.y), 1.0f);
		Painter painter2 = new FudoPainter(new Vector2(ex, centerPoint.y), 1.0f);
		ForcePutPartner(painter1, Team.Player1);
		ForcePutPartner(painter2, Team.Player2);
	}

	public void InitializeSeatStack() {
		seatStack.Clear ();
		seatOrder = 0;
	}
	
	public void UpdateArea(Vector2 centerPoint, float radius, Team team) {
		List<Vector2> updatePoints = new List<Vector2>();
		foreach (Vector2 point in areaTable.Keys) {
			if (Vector2.Distance(centerPoint, point) <= radius) {
				updatePoints.Add (point);
			}
		}

		foreach (Vector2 point in updatePoints) {
			areaTable[point] = team;
		}
	}

	public void AddSeat(Seat seat, Team team) {
		seatOrder++;
		seatStack.Add (seatOrder, seat);

		Debug.Log ("AddSeat: " + team + ", circleList:" + seat.GetCircleList().Count);

		foreach (Circle circle in seat.GetCircleList()) {
			Debug.Log ("circle: " + circle);
			UpdateArea(circle.point, circle.radius, team);
		}
	}

	public int GetSeatOrder() {
		return seatOrder;
	}

	public float CalcurateTeamAreaPercentage(Team targetTeam) {
		int totalCount = 0;
		int targetTeamCount = 0;

		foreach (KeyValuePair<Vector2, Team> entry in areaTable) {
			Team team = (Team)entry.Value;
			if (targetTeam == team) {
				Vector2 point = (Vector2)entry.Key;
				targetTeamCount++;
			}
			totalCount++;
		}

		if (totalCount == 0) {
			return 0f;
		}

		return (float)targetTeamCount / totalCount;
	}

	public Vector2? GetNearPlace(Vector2 position, Team team) {
		// TODO: const
		float distanceThreshold = this.CalculateAreaStep (areaObject, areaDivision);

		foreach (KeyValuePair<Vector2, Team> entry in areaTable) {
			if (team == entry.Value && Vector2.Distance (position, entry.Key) <= distanceThreshold) {
				return entry.Key;
			}
		}

		return null;
	}

	public bool PutPartner(Partner partner, Team team, Vector2 position) {
		Vector2? nearPosition = GetNearPlace (position, team);
		
		if (nearPosition == null) {
			Debug.Log ("Not Found Near partner.");
			return false;
		}

		Painter painter = new FudoPainter(new Vector2(position.x, position.y), 0.5f);
		ForcePutPartner (painter, team);
		
		// goto next turn
		TurnManager.Instance.NextTurnStep();

		return true;
	}

	private void ForcePutPartner(Painter painter, Team team) {
		// area board update
		Seat seat = new Seat(team);
		painter.Paint (seat);
		this.AddSeat (seat, team);
		
		// draw update
		string path = team.PrefabPath();
		Vector3 position = new Vector3(painter.position.x, painter.position.y, areaObject.transform.position.z + 10);
		GameObject stamp = Instantiate(Resources.Load (path), position, Quaternion.identity) as GameObject;
	}

	private float CalculateAreaRadius(GameObject gameObject) {
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		return renderer.bounds.size.x / 2.0f;
	}

	private float CalculateAreaStep(GameObject gameObject, int areaDivision) {
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		return renderer.bounds.size.x / areaDivision;
	}
}