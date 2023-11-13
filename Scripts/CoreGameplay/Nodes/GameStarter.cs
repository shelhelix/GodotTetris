using Godot;

namespace GodotTetris.Scripts;

public partial class GameStarter : Control {
	[Export] public Vector2I     GridSize = new(10, 20);
	[Export]        PlayGridView _playGridView;

	GameManager _gameManager;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		_gameManager = new GameManager();
		_gameManager.ResetGrid(GridSize);
		_gameManager.StartGame();
		_playGridView.Init(_gameManager);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		_gameManager.Fall();
	}
}