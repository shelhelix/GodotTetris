using System;
using System.Linq;
using Godot;

namespace GodotTetris.Scripts;


public class GameManager {
	FallingTetrominoContainer _currentTetromino;
	PlaygroundGrid            _grid;
	int                       _score;
	bool                      _isGameEnded;
	
	public Vector2I PlayAreaSize => new(_grid.SizeX, _grid.SizeY);

	public int Score {
		get => _score;
		private set {
			_score = value;
			OnScoreChanged?.Invoke(_score);	
		}
	}

	bool IsGameEnded {
		get => _isGameEnded;
		set {
			_isGameEnded = value;
			OnGameEnded?.Invoke(_isGameEnded);
		}
	}
	
	public event Action<bool> OnGameEnded;
	public event Action<int>  OnScoreChanged;
	
	public GameManager() {
		ResetGrid(Vector2I.Zero);
		IsGameEnded = true;
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
		ResetGrid(new Vector2I(_grid.SizeX, _grid.SizeY));
		Score = 0;
		CreateNewFallTetrimino();
		IsGameEnded = false;
	}

	public void Fall() {
		if ( IsGameEnded ) {
			return;
		}
		if ( !TryMoveTetrimino(Vector2I.Down) ) {
			EndFall();
		}
	}
	

	public bool TryRotateTetromino() {
		if ( IsGameEnded ) {
			return false;
		}
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
		AddScore(linesRemoved);
	}

	void AddScore(int linesRemoved) {
		switch ( linesRemoved ) {
			case 0:
				return;
			case 1:
				Score += 100;
				break;
			case 2:
				Score += 300;
				break;
			case 3:
				Score += 500;
				break;
			case 4:
				Score += 800;
				break;
		}
		// can't remove more than 4 lines at once
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
			IsGameEnded = true;
		}
	}

	void PlaceFallingTetrominoOnGrid() {
		_grid.SetSubGrid(_currentTetromino.CurrentForm.Grid, _currentTetromino.LeftTopCellPosition);
	}

	bool IsFreePlaceOnGridForForm(Vector2I topLeftCellPos, TetrominoForm form) => _grid.IsSubGridOnGrid(form.Grid, topLeftCellPos) && _grid.IsSubGridEmpty(form.Grid, topLeftCellPos);

	public bool TryMoveTetrimino(Vector2I movement) {
		if ( IsGameEnded ) {
			return false;
		}
		var newLeftTopPos = _currentTetromino.LeftTopCellPosition + movement;
		// check that can place tetrimino grid in new cells
		if ( !IsFreePlaceOnGridForForm(newLeftTopPos, _currentTetromino.CurrentForm) ) {
			return false;
		} 
		_currentTetromino.LeftTopCellPosition = newLeftTopPos;
		return true;
	}
}