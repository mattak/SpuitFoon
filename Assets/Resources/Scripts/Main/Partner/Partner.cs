using UnityEngine;
using System.Collections;

public enum Partner {
	Fudo,
	Pyon,
	Hasir,
}

static class PartnerExt {
	public static string SpritePath(this Partner partner, Team team) {
		string[] pathes = {
			"Sprites/foon_orange_fudo",
			"Sprites/foon_orange_pyon",
			"Sprites/foon_orange_hasir",
			"Sprites/foon_purple_fudo",
			"Sprites/foon_purple_pyon",
			"Sprites/foon_purple_hasir"
		};

		if (team == Team.Player1) {
			return pathes [(int)partner];
		} else if (team == Team.Player2) {
			return pathes [(int)partner + 3]; // TODO: hard cording fix
		}

		return null;
	}

	public static GameObject LoadPartner(this Partner partner, Team team) {
		string path = partner.SpritePath (team);

		if (path == null) {
			return null;
		}

		GameObject prefab = (GameObject)Resources.Load ("Prefabs/Partner");
		Sprite sprite = Resources.Load<Sprite> (path);
		prefab.GetComponent<SpriteRenderer>().sprite = sprite;
		
		return prefab;
	}
	
	private static System.Random random = new System.Random ();

	public static Partner Random() {
		int index = random.Next (3); // TODO: fix hard cording.
		return (Partner)System.Enum.ToObject(typeof(Partner), index);
	}
}