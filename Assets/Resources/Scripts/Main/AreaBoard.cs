using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaBoard : SingletonMonoBehaviourFast<AreaBoard> {
	private Dictionary<Vector2, Team> areaTable; // Vector2, Team
	private List<Seat> seatStack; // Seat
	public int areaDivision = 200;
	public Collider2D areaObject;
	private List<GameObject> debugGrids;

	public void Start() {
		areaTable = new Dictionary<Vector2, Team> ();
		seatStack = new List<Seat> ();
		debugGrids = new List<GameObject>();

		float areaRadius = this.CalculateAreaRadius (areaObject.gameObject);
		float areaStep = this.CalculateAreaStep (areaObject.gameObject, areaDivision);
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
		DrawCircle(painter1, Team.Player1);
		DrawCircle(painter2, Team.Player2);
	}

	public void InitializeSeatStack() {
		seatStack.Clear ();
	}
	
	public void UpdateGrid(Vector2 centerPoint, float radius, Team team) {
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
		seatStack.Add (seat);

		foreach (Circle circle in seat.GetCircleList()) {
			UpdateGrid(circle.point, circle.radius, team);
		}
	}

	public float CalcurateTeamAreaPercentage(Team targetTeam) {
		int totalCount = 0;
		int targetTeamCount = 0;

		foreach (KeyValuePair<Vector2, Team> entry in areaTable) {
			Team team = (Team)entry.Value;
			if (targetTeam == team) {
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
		float distanceThreshold = this.CalculateAreaStep (areaObject.gameObject, areaDivision);

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
			Debug.LogWarning ("Not Found Near partner.");
			return false;
		}

		// new FudoPainter(new Vector2(position.x, position.y), 1.0f);
		Painter painter = partner.CreatePainter(new Vector2(position.x, position.y), 1.0f);
		DrawCircle (painter, team);
		
		// goto next turn
		TurnManager.Instance.NextTurnStep();

		return true;
	}

	public Bounds GetBounds() {
		return this.areaObject.bounds;
	}

	private void DrawDebugGrids() {
		foreach (GameObject obj in debugGrids) {
			Destroy(obj);
		}

		debugGrids.Clear ();
		System.Random random = new System.Random();

		foreach (KeyValuePair<Vector2, Team> pair in areaTable) {
			Vector2 point = pair.Key;
			Team team = pair.Value;

			if (team == Team.Empty && random.Next (50) % 50 != 0) {
				continue;
			}

			GameObject obj = Resources.Load<GameObject> ("Prefabs/Stamp");

			if (team == Team.Player1) {
				obj.GetComponent<SpriteRenderer>().color = Color.red;
			} else if (team == Team.Player2) {
				obj.GetComponent<SpriteRenderer>().color = Color.green;
			} else {
				obj.GetComponent<SpriteRenderer>().color = Color.gray;
			}

			GameObject newObj = (GameObject)Instantiate (obj, new Vector3(point.x, point.y, areaObject.transform.position.z + 9), Quaternion.identity);
			this.debugGrids.Add (newObj);
		}
	}

	private void DrawCircle(Painter painter, Team team) {
		// area board update
		Seat seat = new Seat(team);
		painter.Paint (seat);
		this.AddSeat (seat, team);

		// FIXME: draw update bugs
		// draw update
		seat.Draw (team, areaObject.transform.position.z + 10); // FIXME: hardcording

		// debug
		// DrawDebugGrids();
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