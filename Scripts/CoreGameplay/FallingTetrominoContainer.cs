using Godot;

namespace GodotTetris.Scripts;

public class FallingTetrominoContainer {
	public Vector2I LeftTopCellPosition;
	
	Tetromino _currentTetromino;

	public TetrominoForm CurrentForm => _currentTetromino.CurrentForm;
	
	public TetrominoForm NextForm => _currentTetromino.NextForm;
	
	public FallingTetrominoContainer(Tetromino tetromino, Vector2I startPos) {
		_currentTetromino   = tetromino;
		LeftTopCellPosition = startPos;
	}

	public bool Contains(Vector2I position) =>
		(position.X >= LeftTopCellPosition.X) && (position.X < (LeftTopCellPosition.X + CurrentForm.Grid.GetLength(0))) &&
		(position.Y >= LeftTopCellPosition.Y) && (position.Y < (LeftTopCellPosition.Y + CurrentForm.Grid.GetLength(1)));

	public bool GetCellState(Vector2I position) {
		if ( !Contains(position) ) {
			return false;
		}
		var localPosition = position - LeftTopCellPosition;
		return CurrentForm.Grid[localPosition.X, localPosition.Y];
	}

	public void Rotate() {
		_currentTetromino.Rotate();
	}
}