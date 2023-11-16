using Godot;

namespace GodotTetris.Scripts;

public class PlaygroundGrid {
	bool[,] _grid = new bool[0, 0];

	public int SizeX => _grid.GetLength(0);

	public int SizeY => _grid.GetLength(1);

	public void GeneratePlayground(Vector2I size) {
		_grid = new bool[size.X, size.Y];
	}

	public bool IsCellOnGrid(Vector2I position) => (position.X >= 0) && (position.X < _grid.GetLength(0)) && (position.Y >= 0) && (position.Y < _grid.GetLength(1));

	public bool IsCellEmpty(Vector2I position) => IsCellOnGrid(position) && !_grid[position.X, position.Y];

	public void SetCell(Vector2I position, bool value) {
		if ( IsCellOnGrid(position) ) {
			_grid[position.X, position.Y] = value;
		}
	}

	public bool IsSubGridOnGrid(bool[,] subGrid, Vector2I leftTopCornerPos) {
		for ( var x = 0; x < subGrid.GetLength(0); x++ ) {
			for ( var y = 0; y < subGrid.GetLength(1); y++ ) {
				var cellPos = leftTopCornerPos + new Vector2I(x, y);
				if ( subGrid[x, y] && !IsCellOnGrid(cellPos) ) {
					return false;
				}
			}
		}
		return true;
	}

	public bool IsSubGridEmpty(bool[,] subGrid, Vector2I leftTopCornerPos) {
		for ( var x = 0; x < subGrid.GetLength(0); x++ ) {
			for ( var y = 0; y < subGrid.GetLength(1); y++ ) {
				var cellPos = leftTopCornerPos + new Vector2I(x, y);
				if ( subGrid[x,y] && (!IsCellOnGrid(cellPos) || !IsCellEmpty(cellPos)) ) {
					return false;
				}
			}
		}
		return true;
	}

	public void SetSubGrid(bool[,] subGrid, Vector2I leftTopCornerPos) {
		if ( !IsSubGridOnGrid(subGrid, leftTopCornerPos) || !IsSubGridEmpty(subGrid, leftTopCornerPos) ) {
			GD.PrintErr("Something is wrong. Sub grid is not empty or not on grid at all");
			return;
		}
		for ( var x = 0; x < subGrid.GetLength(0); x++ ) {
			for ( var y = 0; y < subGrid.GetLength(1); y++ ) {
				var cellPos = leftTopCornerPos + new Vector2I(x, y);
				if ( subGrid[x, y] ) {
					SetCell(cellPos, subGrid[x, y]); 
				}
			}
		}
	}

	// false - empty; true - filled
	public bool GetCellState(Vector2I position) => !IsCellEmpty(position) && !IsCellEmpty(position);
}