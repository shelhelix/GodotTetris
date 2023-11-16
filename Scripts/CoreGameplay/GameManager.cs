using System;
using System.Linq;
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

	public bool GetCellState(Vector2I position) {
		if ( _currentTetromino.Contains(position) && _currentTetromino.GetCellState(position) ) {
			return _currentTetromino.GetCellState(position);
		}
		return _grid.GetCellState(position);
	}

	public void StartGame() {
		if ( _isGameEnded ) {
			ResetGrid(new Vector2I(_grid.SizeX, _grid.SizeY));
		}
		CreateNewFallTetrimino();
		_isGameEnded = false;
	}

	public void Fall() {
		if ( _isGameEnded ) {
			return;
		}
		if ( !TryMoveTetrimino(Vector2I.Down) ) {
			EndFall();
		}
	}
	

	public bool TryRotateTetromino() {
		if ( !IsFreePlaceOnGridForForm(_currentTetromino.LeftTopCellPosition, _currentTetromino.NextForm) ) {
			return false;
		} 
		_currentTetromino.Rotate();
		return true;
	}

	void EndFall() {
		PlaceFallingTetrominoOnGrid();
		ClearFilledLines();
		CreateNewFallTetrimino();
	}

	void ClearFilledLines() {
		var linesRemoved = 0;
		foreach ( var y in GD.Range(_grid.SizeY) ) {
			if ( IsLineFilled(y) ) {
				RemoveLine(y);
				linesRemoved++;
			}
		}
		GD.Print($"Lines combo: {linesRemoved}");
	}

	bool IsLineFilled(int yPos) {
		return GD.Range(_grid.SizeX).All(x => !_grid.IsCellEmpty(new Vector2I(x, yPos)));
	}

	void RemoveLine(int yPos) {
		foreach ( var x in GD.Range(_grid.SizeX) ) {
			// remove all cells from line
			_grid.SetCell(new Vector2I(x, yPos), false);
		}
		foreach ( var y in GD.Range(yPos-1, -1, -1) ) {
			foreach ( var x in GD.Range(_grid.SizeX) ) {
				// move all cells above line down
				_grid.SetCell(new Vector2I(x, y+1), _grid.GetCellState(new Vector2I(x, y)));
			}
		}
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

	public bool TryMoveTetrimino(Vector2I movement) {
		var newLeftTopPos = _currentTetromino.LeftTopCellPosition + movement;
		// check that can place tetrimino grid in new cells
		if ( !IsFreePlaceOnGridForForm(newLeftTopPos, _currentTetromino.CurrentForm) ) {
			return false;
		} 
		_currentTetromino.LeftTopCellPosition = newLeftTopPos;
		return true;
	}
}