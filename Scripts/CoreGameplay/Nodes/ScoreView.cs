using Godot;

namespace GodotTetris.Scripts; 

public partial class ScoreView : Label {
	GameManager _gameManager;
	
	public void Init(GameManager gameManager) {
		_gameManager                =  gameManager;
		_gameManager.OnScoreChanged += OnScoreChanged;
		_gameManager.OnGameEnded    += OnGameStateChanged;
		OnScoreChanged(_gameManager.Score);
	}

	void OnGameStateChanged(bool isEnded) {
		Text = isEnded ? $"Score: {_gameManager.Score} \nGame over" : $"Score: {_gameManager.Score}";
	}

	void OnScoreChanged(int score) {
		Text = $"Score: {score}";		
	}
}