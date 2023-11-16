using System;
using System.Collections.Generic;
using Godot;

namespace GodotTetris.Scripts; 

public static class TetrominoFactory {
	static List<Func<Tetromino>> _tetrominoCreators = new() {
		CreateL,
		CreateJ,
		CreateI,
		CreateSquare,
		CreateZ,
		CreateS,
		CreateT
	};
	
	public static Tetromino CreateRandomTetromino() {
		var randomIndex = (int) (GD.Randi() % _tetrominoCreators.Count);
		return _tetrominoCreators[randomIndex].Invoke();
	}
	static Tetromino CreateJ() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" X",
				" X",
				"XX",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"X  ",
				"XXX",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" XX",
				" X ",
				" X ",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"   ",
				"XXX",
				"  X",
			})
		);
		return tetromino;
	}
	static Tetromino CreateL() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" X ",
				" X ",
				" XX",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"   ",
				"XXX",
				"X  ",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"XX",
				" X",
				" X",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"  X",
				"XXX",
			})
		);
		return tetromino;
	}
	
	
	static Tetromino CreateZ() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"XX ",
				" XX",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"  X",
				" XX",
				" X ",
			})
		);
		return tetromino;
	}
	
	static Tetromino CreateS() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" XX",
				"XX ",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"X ",
				"XX",
				" X",
			})
		);
		return tetromino;
	}
	
	static Tetromino CreateI() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" X",
				" X",
				" X",
				" X",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"    ",
				"XXXX",
			})
		);
		return tetromino;
	}
	
	static Tetromino CreateSquare() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"XX",
				"XX",
			})
		);
		return tetromino;
	}
	
	
	static Tetromino CreateT() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" X ",
				"XXX",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"X ",
				"XX",
				"X ",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"XXX",
				" X ",
			})
		);
		tetromino.AddForm(
			new TetrominoForm(new[] {
				" X",
				"XX",
				" X",
			})
		);
		return tetromino;
	}
}