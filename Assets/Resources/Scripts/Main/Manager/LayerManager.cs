using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class LayerManager : SingletonMonoBehaviourFast<LayerManager> {
	public GameObject startLayer;
	public GameObject resultLayer;
	public GameObject playingLayer;
	public ReactiveProperty<GamePhase> Layer = new ReactiveProperty<GamePhase>(GamePhase.Start);
	private ActiveViewModel startLayerModel;
	private ActiveViewModel resultLayerModel;
	private ActiveViewModel playingLayerModel;

	// Use this for initialization
	void Start () {
		startLayerModel = new ActiveViewModel(Layer.Select(type => type == GamePhase.Start));
		startLayerModel.Subscribe (startLayer);
		playingLayerModel = new ActiveViewModel(Layer.Select(type => type == GamePhase.Playing));
		playingLayerModel.Subscribe (playingLayer);
		resultLayerModel = new ActiveViewModel(Layer.Select(type => type == GamePhase.Result));
		resultLayerModel.Subscribe (resultLayer);
	}
}