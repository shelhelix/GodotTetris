using System.Collections.Generic;
using Godot;
using GodotTetris.Scripts.Nodes;

namespace GodotTetris.Scripts; 

public partial class PlayGridView : GridContainer {
	[Export]
	PackedScene _tetriminoViewPrefab;

	[Export]
	GridContainer _container;

	List<TetriminoView> _instances = new();
	
	GameManager _gameManager;
	
	public void Init(GameManager gameManager) {
		_gameManager = gameManager;
		var size = _gameManager.PlayAreaSize;
		for ( var x = 0; x < size.X; x++ ) {
			for ( var y = 0; y < size.Y; y++ ) {
				var instance = _tetriminoViewPrefab.Instantiate() as TetriminoView;
				_container.AddChild(instance);
				_instances.Add(instance);
				instance.SetState(false);
				instance.Size              = new Vector2(1080/size.Y, 1080/size.Y);
				instance.CustomMinimumSize = instance.Size;
			}
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if ( _gameManager == null ) {
			return;
		}
		var size = _gameManager.PlayAreaSize;
		for ( var x = 0; x < size.X; x++ ) {
			for ( var y = 0; y < size.Y; y++ ) {
				var position = new Vector2I(x, y);
				_instances[x * size.Y + y].SetState(_gameManager.GetCellState(position));
			}
		}
	}
}
