using Godot;
using System;

public partial class pixel_test : Node3D
{
	private CanvasLayer _photoMode;
	bool photo = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_photoMode = GetNode<CanvasLayer>("PixelContainer/PhotoMode");
		_photoMode.Visible = false;
		photo = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("camera_mode") && photo == false)
		{
			_photoMode.Visible = true;
			photo = true;
		}
		else if (Input.IsActionJustPressed("camera_mode") && photo == true)
		{
			_photoMode.Visible = false;
			photo = false;
		}
	}
}
