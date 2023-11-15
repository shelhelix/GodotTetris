using System.Collections.Generic;
using Godot;
using GodotTetris.Scripts.Nodes;

namespace GodotTetris.Scripts; 

public partial class PlayGridView : Control{
	[Export] PackedScene _tetriminoViewPrefab;

	[Export] VBoxContainer _verticalContainer;
	

	List<TetriminoView> _instances = new();
	
	GameManager _gameManager;
	
	public void Init(GameManager gameManager) {
		_gameManager = gameManager;
		var size       = _gameManager.PlayAreaSize;
		var cellSideSize = 1080 / size.Y;
		for ( var y = 0; y < size.Y; y++ ) {
			var horizontalContainer = new HBoxContainer();
			horizontalContainer.CustomMinimumSize   = new Vector2(0, cellSideSize);
			horizontalContainer.SizeFlagsHorizontal = SizeFlags.Fill;
			horizontalContainer.AddThemeConstantOverride("separation", 0);
			horizontalContainer.Alignment = BoxContainer.AlignmentMode.Center;
			_verticalContainer.AddChild(horizontalContainer);
			for ( var x = 0; x < size.X; x++ ) {
				var instance = _tetriminoViewPrefab.Instantiate() as TetriminoView;
				if ( instance == null ) {
					GD.PrintErr("Something went wrong. Node is not supported");
					return;
				}
				horizontalContainer.AddChild(instance);
				_instances.Add(instance);
				instance.SetState(false);
				instance.Size              = Vector2.One * cellSideSize;
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
				_instances[y * size.X + x].SetState(_gameManager.GetCellState(position));
			}
		}
	}
}
