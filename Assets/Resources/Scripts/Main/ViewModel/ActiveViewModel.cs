using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class ActiveViewModel {
	private IObservable<bool> trigger;

	public ActiveViewModel(IObservable<bool> trigger) {
		this.trigger = trigger;
	}

	public void Subscribe(GameObject gameObject) {
		this.trigger.Subscribe(result => {
			gameObject.SetActive(result);
		});
	}
}