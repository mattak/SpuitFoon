using UnityEngine;
using System.Collections;

public enum TurnStep {
	Setup,
	PlayerAction1,
	PlayerAction2,
	Cleanup
}

static class TurnExt {
	public static TurnStep GetNext(this TurnStep step) {
		int nextIndex = ((int)step + 1) % 4; // TODO FIX hard cording.
		return (TurnStep)System.Enum.ToObject(typeof(TurnStep), nextIndex);
	}
}