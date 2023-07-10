using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
		var keyEvent = @event as InputEventKey;
		if (keyEvent == null)
			return;

		var key = keyEvent.GetKeycodeWithModifiers();
		var step = 15;

		if (key == Key.Up)
		{
            Offset += new Vector2(0, -step);
        }
		else if (key == Key.Down)
		{
            Offset += new Vector2(0, +step);
        }
		else if (key == Key.Right)
		{
            Offset += new Vector2(+step, 0);
        }
		else if (key == Key.Left)
		{
            Offset += new Vector2(- step, 0);
        }

		var zoomStep = 0.1F;
		var minZoom = new Vector2(0.4f, 0.4f);
		var maxZoom = new Vector2(2.5f, 2.5f);
		if (key == Key.Minus && Zoom > minZoom)
		{
            Zoom -= new Vector2(zoomStep, zoomStep);
        }
		else if (key == Key.Equal && Zoom < maxZoom) 
		{
            Zoom += new Vector2(zoomStep, zoomStep);
        }
    }
}