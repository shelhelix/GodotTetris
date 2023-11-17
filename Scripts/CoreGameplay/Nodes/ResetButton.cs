using Godot;

namespace GodotTetris.Scripts; 

public partial class ResetButton : Button {
	public void Init(GameManager gameManager) {	
		Pressed += gameManager.StartGame;
	}
}