using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class TouchPaintBehaviour : MonoBehaviour {
	public float scale = 1.0f;
	public GameObject parentObject;

	// Use this for initialization
	void Start () {
		Observable
				.EveryUpdate()
				.Where (_ => Input.GetMouseButtonDown(0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (p => new Vector3(p.x, p.y, parentObject.transform.position.z))
				.Subscribe (position => {
					GameObject stamp = Instantiate(Resources.Load ("Prefabs/Stamp"), position, Quaternion.identity) as GameObject;
					stamp.transform.parent = parentObject.transform;
				});
	}
}