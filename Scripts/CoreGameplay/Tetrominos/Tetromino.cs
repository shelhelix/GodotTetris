using System.Collections.Generic;
using GodotTetris.Scripts.Utils;

namespace GodotTetris.Scripts;

public class Tetromino {
	List<TetrominoForm> _forms = new();
	int                 _currentFormIndex;

	int NextFormIndex => (_currentFormIndex + 1) % _forms.Count;
	
	public TetrominoForm CurrentForm => _forms.GetOrDefault(_currentFormIndex);
	public TetrominoForm NextForm    => _forms.GetOrDefault(NextFormIndex);

	public void AddForm(TetrominoForm form) {
		if ( form == null ) {
			return;
		}
		_forms.Add(form);
	}
	
	public void Rotate() {
		_currentFormIndex = NextFormIndex;
	}
}