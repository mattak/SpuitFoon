using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ViewModel
{
	public static class GameObjectExtensions
	{
		public static IObservable<Collider2D> OnMouseDownCollider2DAsObservale (this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour
				.UpdateAsObservable ()
				.Where (_ => Input.GetMouseButtonDown (0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition))
				.Select (point => Physics2D.OverlapPoint (point));
		}

		public static IObservable<Vector3> OnMouseMoveWorldPointAsObservable (this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour
				.UpdateAsObservable ()
				.Where (_ => PickerManager.Instance.CanPutdownPartner ())
				.Where (_ => !Input.GetMouseButtonDown (0) && !Input.GetMouseButtonUp (0) && Input.GetMouseButton (0))
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}

		public static IObservable<Vector3> OnMouseUpWorldPointAsObservable (this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour
				.UpdateAsObservable ()
				.Where (_ => Input.GetMouseButtonUp (0))
				.Where (_ => PickerManager.Instance.CanPutdownPartner ())
				.Select (_ => Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
	}
}