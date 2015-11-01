using UnityEngine;
using System;
using System.Collections;

public class PyonPainter: Painter {
	float radius;

	public PyonPainter(Vector2 position, float radius) : base(position) {
		this.radius = radius;
	}

	public override void Paint(Seat seat) {
		Bounds bounds = AreaBoardManager.Instance.GetBounds ();

		System.Random random = new System.Random();
		float px = (float)random.NextDouble () * bounds.size.x;
		float py = (float)random.NextDouble () * bounds.size.y;
		float nx = px - bounds.center.x - bounds.size.x/2;
		float ny = py - bounds.center.y - bounds.size.y/2;

		Vector2 newPosition = new Vector2(nx, ny);
		seat.AddCircle (new Circle(newPosition, radius));
	}
}