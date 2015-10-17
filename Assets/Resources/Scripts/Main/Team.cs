using UnityEngine;
using System.Collections;

public enum Team {
	Player1,
	Player2,
	Empty
}

static class TeamExt {
	public static string PrefabPath(this Team team) {
		string[] pathes = {"Prefabs/StampPlayer1", "Prefabs/StampPlayer2", ""};
		return pathes[(int)team];
	}
}