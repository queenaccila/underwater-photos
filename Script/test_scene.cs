using Godot;
using System;

public partial class test_scene : Node3D
{
	[Signal] public delegate void FishCheckedEventHandler(int fishNum);
	bool photoMode = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//filters out types of creatures in photos
	private void _on_player_fish_enter(Area3D area)
	{
		if(Input.IsActionJustPressed("camera_mode")){
			//add more groups later on with if statements
			if(area.IsInGroup("test")){
				EmitSignal(SignalName.FishChecked, 0);
			}
		}
	}

	private void _on_player_fish_exit(Area3D area)
	{
		if(area.IsInGroup("test")){
			//add more stuff
		}
	}
}
