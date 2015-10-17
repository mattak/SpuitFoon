using UnityEngine;
using System.Collections;

public class AreaBoard : SingletonMonoBehaviourFast<AreaBoard> {
	private Hashtable areaTable; // Vector2, Team
	private Hashtable seatStack; // Int, Seat
	private int seatOrder;

	public void Start() {
		areaTable = new Hashtable ();
		seatStack = new Hashtable ();
		seatOrder = 0;
	}

	// TODO: change flexibility for 
	public void InitializeArea(Vector2 centerPoint, float radius, float step) {
		areaTable.Clear ();
		float sx = centerPoint.x - radius;
		float sy = centerPoint.y - radius;
		float ex = centerPoint.x + radius;
		float ey = centerPoint.y + radius;

		for (float y = sy; y < ey; y += step) {
			for (float x = sx; x < ex; x += step) {
				Vector2 point = new Vector2 (x, y);
				if (Vector2.Distance (centerPoint, point) <= radius) {
					areaTable.Add (point, Team.Empty);
				}
			}
		}
	}

	public void InitializeSeatStack() {
		seatStack.Clear ();
		seatOrder = 0;
	}
	
	public void UpdateArea(Vector2 centerPoint, float radius, Team team) {
		foreach (Vector2 point in areaTable.Keys) {
			if (Vector2.Distance(centerPoint, point) <= radius) {
				areaTable.Add (point, team);
			}
		}
	}

	public void AddSeat(Seat seat, Team team) {
		seatOrder++;
		seatStack.Add (seatOrder, seat);

		foreach (Circle circle in seat.GetCircleList()) {
			UpdateArea(circle.point, circle.radius, team);
		}
	}

	public int GetSeatOrder() {
		return seatOrder;
	}

	public float CalcurateTeamAreaPercentage(Team targetTeam) {
		int totalCount = 0;
		int targetTeamCount = 0;

		foreach (DictionaryEntry entry in areaTable) {
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
}