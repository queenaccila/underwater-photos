using Godot;
using System;

public partial class journal : CanvasLayer
{
	private TextureRect _icon1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_icon1 = GetNode<TextureRect>("VBoxContainer/Task1/Icon-Task");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_tree_entered()
	{
		if(globalVar.fishType == 0){
			Texture2D texture = (Texture2D)GD.Load<Texture>("res://underwater-photos/Art/ui/camera-ui-checked.png");
			_icon1.Texture = texture;
		}
	}
}
