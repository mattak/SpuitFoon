using UnityEngine;
using System.Collections;

public class FudoPainter: Painter {
	Vector2 position;
	float radius;

	public FudoPainter(Vector2 position, float radius) {
		this.position = position;
		this.radius = radius;
	}

	public void Paint(Seat seat) {
		seat.AddCircle (new Circle(position, radius));
	}
}