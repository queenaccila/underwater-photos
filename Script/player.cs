using Godot;
using System;

public partial class player : CharacterBody3D
{
	[Export] public float SwimSpeed = 3.0f; // Speed for horizontal swimming
	[Export] public float SwimUpSpeed = 2.0f; // Speed for swimming up
	[Export] public float SinkSpeed = 1.0f; // Speed at which the player sinks
	[Export] public float WaterResistance = 1.0f; // Resistance to simulate water drag
	[Export] public float MouseSensitivity = 0.1f; // Sensitivity for mouse movement
	[Signal] public delegate void PhotoModeEventHandler();

	private Camera3D _camera;
	public Node photoScene;
	private RayCast3D _raycast;
	

	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("Camera3D"); // Ensure you have a Camera3D node as a child of the player
		Input.MouseMode = Input.MouseModeEnum.Captured; // Capture the mouse for free look
		_raycast = GetNode<RayCast3D>("RayCast3D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Apply sinking effect when no input is given for swimming up
		if (Input.IsActionPressed("up"))
		{
			velocity.Y = SwimUpSpeed;
		}
		else
		{
			velocity.Y -= SinkSpeed * (float)delta;
		}

		// Get the input direction for horizontal movement.
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		// Apply swimming movement
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * SwimSpeed;
			velocity.Z = direction.Z * SwimSpeed;
		}

		// Apply water resistance to slow down movement when not actively swimming
		velocity.X = Mathf.MoveToward(velocity.X, 0, WaterResistance * (float)delta);
		velocity.Z = Mathf.MoveToward(velocity.Z, 0, WaterResistance * (float)delta);

		Velocity = velocity;
		MoveAndSlide();
		
		if(Input.IsActionJustPressed("camera_mode"))
		{
			//pop up camera menu
			EmitSignal(SignalName.PhotoMode);
			
			//test raycast
			if(_raycast.IsColliding())
			{
				GD.Print("collided");
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		// Define the higher mouse sensitivity factor
		float mouseMovement = 3f;
		
		if (@event is InputEventMouseMotion mouseEvent)
		{
			// Rotate the player around the Y axis (yaw)
			RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * MouseSensitivity * mouseMovement));

			// Rotate the camera around the X axis (pitch)
			_camera.RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * MouseSensitivity * mouseMovement));

			// Clamp the camera rotation to prevent flipping
			Vector3 cameraRotation = _camera.RotationDegrees;
			cameraRotation.X = Mathf.Clamp(cameraRotation.X, -90, 90);
			_camera.RotationDegrees = cameraRotation;
		}
	}
}
