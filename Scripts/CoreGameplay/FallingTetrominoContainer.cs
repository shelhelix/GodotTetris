using Godot;

namespace GodotTetris.Scripts;

public class FallingTetrominoContainer {
	public Vector2I LeftTopCellPosition;
	
	Tetromino _currentTetromino;

	public TetrominoForm CurrentForm => _currentTetromino.CurrentForm;
	
	public FallingTetrominoContainer(Tetromino tetromino, Vector2I startPos) {
		_currentTetromino   = tetromino;
		LeftTopCellPosition = startPos;
	}
}