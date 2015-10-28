using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public enum GameLayer {
	Start,
	Playing,
	Result,
};

public class LayerManager : SingletonMonoBehaviourFast<LayerManager> {
	public GameObject startLayer;
	public GameObject resultLayer;
	public GameObject playingLayer;
	public ReactiveProperty<GameLayer> layer = new ReactiveProperty<GameLayer>(GameLayer.Start);
	private ActiveViewModel startLayerModel;
	private ActiveViewModel resultLayerModel;
	private ActiveViewModel playingLayerModel;

	// Use this for initialization
	void Start () {
		startLayerModel = new ActiveViewModel(layer.Select(type => type == GameLayer.Start));
		startLayerModel.Subscribe (startLayer);
		playingLayerModel = new ActiveViewModel(layer.Select(type => type == GameLayer.Playing));
		playingLayerModel.Subscribe (playingLayer);
		resultLayerModel = new ActiveViewModel(layer.Select(type => type == GameLayer.Result));
		resultLayerModel.Subscribe (resultLayer);
	}
}