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

	public static Team Opposite(this Team team) {
		if (team == Team.Player1) {
			return Team.Player2;
		} else if (team == Team.Player2) {
			return Team.Player1;
		} else {
			return Team.Empty;
		}
	}

	public static Team Random() {
		System.Random random = new System.Random ();
		int index = random.Next (2);
		return (Team)System.Enum.ToObject (typeof(Team), index);
	}
}