using UnityEngine;
using System.Collections;

public abstract class Painter {
	public Vector2 position;

	public Painter(Vector2 position) {
		this.position = position;
	}

	abstract public void Paint (Seat seat);
}