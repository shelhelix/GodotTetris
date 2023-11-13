using System;
using Godot;

namespace GodotTetris.Scripts;


public class GameManager {
	FallingTetrominoContainer _currentTetromino;
	
	PlaygroundGrid _grid;

	public Vector2I PlayAreaSize => new(_grid.SizeX, _grid.SizeY);
	
	public event Action OnGameEnded;

	bool _isGameEnded;
	
	public GameManager() {
		ResetGrid(Vector2I.Zero);
		_isGameEnded = true;
	}

	public void ResetGrid(Vector2I size) {
		_grid = new PlaygroundGrid();
		_grid.GeneratePlayground(size);
	}

	public bool GetCellState(Vector2I position) => _grid.GetCellState(position);

	public void StartGame() {
		if ( _isGameEnded ) {
			ResetGrid(new Vector2I(_grid.SizeX, _grid.SizeY));
		}
		CreateNewFallTetrimino();
	}

	public void Fall() {
		if ( _isGameEnded ) {
			return;
		}
		var newLeftTopPos = _currentTetromino.LeftTopCellPosition + Vector2I.Down;
		// check that can place tetrimino grid in new cells
		if ( !IsFreePlaceOnGridForForm(newLeftTopPos, _currentTetromino.CurrentForm) ) {
			EndFall();
		} else {
			_currentTetromino.LeftTopCellPosition = newLeftTopPos;
		}
	}

	void EndFall() {
		PlaceFallingTetrominoOnGrid();
		CreateNewFallTetrimino();
	}

	void CreateNewFallTetrimino() {
		var selectedTetromino = TetrominoFactory.CreateRandomTetromino();
		_currentTetromino = new FallingTetrominoContainer(selectedTetromino, new Vector2I(_grid.SizeX / 2, 0));
		if ( !IsFreePlaceOnGridForForm(_currentTetromino.LeftTopCellPosition, selectedTetromino.CurrentForm) ) {
			_isGameEnded = true;
			OnGameEnded?.Invoke();
		}
	}

	void PlaceFallingTetrominoOnGrid() {
		_grid.SetSubGrid(_currentTetromino.CurrentForm.Grid, _currentTetromino.LeftTopCellPosition);
	}

	bool IsFreePlaceOnGridForForm(Vector2I topLeftCellPos, TetrominoForm form) => _grid.IsSubGridOnGrid(form.Grid, topLeftCellPos) && _grid.IsSubGridEmpty(form.Grid, topLeftCellPos);
}