﻿using System;
using Godot;

namespace GodotTetris.Scripts;

public class TetrominoForm {
	public bool[,] Grid { get; }

	public TetrominoForm(string[] lines) {
		if ( (lines == null) || (lines.Length == 0) ) {
			GD.PrintErr("Can't init form. Array is empty");
			return;
		}
		var firstLineSize = lines[0].Length;
		if (Array.Exists(lines, line => line.Length != firstLineSize)) {
			GD.PrintErr("Can't init form. Lines have different sizes");
			return;
		}
		Grid = new bool[firstLineSize, lines.Length];
		for (var y = 0; y < lines.Length; y++) {
			for (var x = 0; x < lines[y].Length; x++) {
				Grid[x, y] = lines[y][x] == 'X';
			}
		}
	}
}