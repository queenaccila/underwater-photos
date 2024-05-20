using Godot;
using System;

public partial class fish : Node3D
{
	private AnimationTree _animTree;
	private AnimationPlayer _animPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animTree = GetNode<AnimationTree>("AnimationTree");
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		_animPlayer.Play("ArmatureAction");

		// Assuming your animation tree has a state machine and "ArmatureAction" is a state
		//var stateMachine = (AnimationNodeStateMachinePlayback)_animTree.Get("parameters/playback");
		//stateMachine.Start("ArmatureAction-loop");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
