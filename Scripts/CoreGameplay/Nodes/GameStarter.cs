using Godot;

namespace GodotTetris.Scripts;

public partial class GameStarter : Control {
	const float UpdateInterval = 0.5f;
	
	[Export] public Vector2I     GridSize = new(10, 20);
	[Export]        PlayGridView _playGridView;

	GameManager _gameManager;
	
	double _timeSinceLastUpdate = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		_gameManager = new GameManager();
		_gameManager.ResetGrid(GridSize);
		_gameManager.StartGame();
		_playGridView.Init(_gameManager);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
