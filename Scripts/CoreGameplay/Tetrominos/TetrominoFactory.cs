namespace GodotTetris.Scripts; 

public static class TetrominoFactory {
	public static Tetromino CreateRandomTetromino() {
		return CreateSquare();
	}
	
	public static Tetromino CreateSquare() {
		var tetromino = new Tetromino();
		tetromino.AddForm(
			new TetrominoForm(new[] {
				"XX",
				"XX",
			})
		);
		return tetromino;
	}
}