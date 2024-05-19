using Godot;
using System;

public partial class pixel_test : Node3D
{
	private CanvasLayer _photoMode;
	private TextureRect _screenshotDisplay;
	private Viewport _viewport;
	private ColorRect _blackRect;
	private AudioStreamPlayer _click;
	private AudioStreamPlayer _ambience;
	private AudioStreamPlayer _music;
	bool photo = false;
	
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
		
		_blackRect = GetNode<ColorRect>("PixelContainer/PixelViewport/ColorRect");
		_blackRect.Visible = false;
		
		//Audio loading
		_click = GetNode<AudioStreamPlayer>("Audio/Click");
		_ambience = GetNode<AudioStreamPlayer>("Audio/Ambience");
		_music = GetNode<AudioStreamPlayer>("Audio/Music");
		
		//Play audio
		_ambience.Play();
		_music.Play();
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
			Flash();
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
	}
	
	private void Flash()
	{
		_click.Play();
		
		// Define colors for the tween
		Color transparentColor = new Color(0f, 0f, 0f, 0f); // Fully transparent
		Color opaqueColor = new Color(0f, 0f, 0f, 1f); // Fully opaque (black)
		
		_blackRect.Visible = true; // Make sure the ColorRect is visible

		// Create the tween
		Tween tween = GetTree().CreateTween();

		// Tween to black (opaque)
		tween.TweenProperty(_blackRect, "modulate", opaqueColor, 0.1f);

		// Tween back to transparent
		tween.TweenProperty(_blackRect, "modulate", transparentColor, 0.1f).SetDelay(0.05f);
	}

	//signal function for PhotoTaken
	private void _on_photo_taken()
	{
		//GD.Print("photo");
	}
}
