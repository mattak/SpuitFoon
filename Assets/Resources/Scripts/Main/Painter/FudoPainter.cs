using UnityEngine;
using System.Collections;

public class FudoPainter: Painter {
	float radius;

	public FudoPainter(Vector2 position, float radius) : base(position) {
		this.radius = radius;
	}

	public override void Paint(Seat seat) {
		seat.AddCircle (new Circle(position, radius));
	}
}