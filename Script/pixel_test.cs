using Godot;
using System;

public partial class pixel_test : Node3D
{
	private CanvasLayer _photoMode;
	private TextureRect _screenshotDisplay;
	private Viewport _viewport;
	bool photo = false;
	private int screenshotCount = 0;
	
	[Signal]
	public delegate void PhotoTakenEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_photoMode = GetNode<CanvasLayer>("PixelContainer/PhotoMode");
		_photoMode.Visible = false;
		photo = false;
		
		_screenshotDisplay = GetNode<TextureRect>("CanvasLayer/PhotoMode/ScreenshotDisplay");
		_viewport = GetNode<Viewport>("PixelContainer/PixelViewport");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//pulls up camera mode
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
		
		//takes a photo
		if(Input.IsActionJustPressed("take_photo") && photo == true)
		{
			//GD.Print("took photo");
			TakeScreenshot();
		}
	}

	private void TakeScreenshot()
	{
		// Capture the image from the viewport
		Image img = _viewport.GetTexture().GetImage();
		
		// Specify the save path with file name and extension
		var filePath = "res://underwater-photos/Screenshots/screenshot.png";
		
		// Save the image as a PNG
		img.SavePng(filePath);

		EmitSignal(SignalName.PhotoTaken);
	}
	
	private void _on_photo_taken()
	{
		GD.Print("photo");
	}
}
