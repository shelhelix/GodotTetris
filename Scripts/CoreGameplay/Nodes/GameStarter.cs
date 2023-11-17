using Godot;

namespace GodotTetris.Scripts;

public partial class GameStarter : Control {
	const float UpdateInterval = 0.5f;
	
	[Export] Vector2I     _gridSize = new(10, 20);
	[Export] PlayGridView _playGridView;
	[Export] ScoreView    _scoreView;
	[Export] ResetButton  _resetButton; 
	

	GameManager _gameManager;
	
	double _timeSinceLastUpdate;
	
	public override void _Ready() {
		_gameManager = new GameManager();
		_gameManager.ResetGrid(_gridSize);
		_gameManager.StartGame();
		_playGridView.Init(_gameManager);
		_scoreView.Init(_gameManager);
		_resetButton.Init(_gameManager);
	}

	public override void _Process(double delta) {
		_timeSinceLastUpdate += delta;
		if ( _timeSinceLastUpdate > UpdateInterval ) {
			_gameManager.Fall();
			_timeSinceLastUpdate = 0;
		}
	}

	public override void _Input(InputEvent ev) {
		base._Input(ev);

		if ( Input.IsKeyPressed(Key.Left) ) {
			_gameManager.TryMoveTetrimino(Vector2I.Left);
		}
		
		if ( Input.IsKeyPressed(Key.Right) ) {
			_gameManager.TryMoveTetrimino(Vector2I.Right);
		}
		
		if ( Input.IsKeyPressed(Key.Down) ) {
			_gameManager.TryMoveTetrimino(Vector2I.Down);
		}
		
		if ( Input.IsKeyPressed(Key.Up) ) {
			_gameManager.TryRotateTetromino();
		}
	}
}
