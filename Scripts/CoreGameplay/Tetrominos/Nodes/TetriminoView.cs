using Godot;

namespace GodotTetris.Scripts.Nodes; 

public partial class TetriminoView : TextureRect {
	[Export]
	TextureRect _activeSprite;

	[Export] Texture2D _emptyCell;
	[Export] Texture2D _filledCell;

	public void SetState(bool isFilled) {
		_activeSprite.Texture = isFilled ? _filledCell : _emptyCell;
	}
}