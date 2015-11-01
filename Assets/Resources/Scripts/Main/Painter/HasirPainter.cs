using UnityEngine;
using System.Collections;

public class HasirPainter: Painter {
	float radius;

	public HasirPainter(Vector2 position, float radius) : base(position) {
		this.radius = radius / 10.0f;
	}

	public override void Paint(Seat seat) {
		Bounds bounds = AreaBoardManager.Instance.GetBounds ();

		System.Random random = new System.Random();
		float px, py, nx, ny;
		bool useX = Mathf.Abs(position.x - bounds.center.x) >= Mathf.Abs(position.y - bounds.center.y);
		bool useMax = useX ? (position.x - bounds.center.x) < 0 : (position.y - bounds.center.y) < 0;

		if (useX) {
			px = useMax ? bounds.size.x : 0;
			py = (float)random.NextDouble () * bounds.size.y;
		} else {
			px = (float)random.NextDouble () * bounds.size.x;
			py = useMax ? bounds.size.y : 0;
		}

		nx = px - bounds.center.x - bounds.size.x/2;
		ny = py - bounds.center.y - bounds.size.y/2;

		Vector2 newPosition = new Vector2(nx, ny);

		PaintLine (seat, position, newPosition);
	}

	private void PaintLine(Seat seat, Vector2 from, Vector2 to) {
		float distance = Vector2.Distance (from, to);

		// FIXME: avoid div 0.
		if (distance <= 0) {
			distance = 0.001f;
		}

		int step = (int)((distance/ radius) * 2.0f + 1);
		float stepX = (to.x - from.x) / step;
		float stepY = (to.y - from.y) / step;

		for (int i = 0; i<step; i++) {
			Vector2 newPosition = new Vector2(from.x + stepX * i, from.y + stepY * i);
			seat.AddCircle (new Circle(newPosition, radius));
		}
	}
}