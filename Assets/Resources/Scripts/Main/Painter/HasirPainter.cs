using UnityEngine;
using System.Collections;

public class HasirPainter: Painter {
	float radius;

	public HasirPainter(Vector2 position, float radius) : base(position) {
		this.radius = radius / 10.0f;
	}

	public override void Paint(Seat seat) {
		seat.AddCircle (new Circle(position, radius));
	}
}