using Godot;
using System;

public partial class CameraAnchor : ColorRect
{
    public override void _Input(InputEvent @event)
    {
		var keyEvent = @event as InputEventKey;
		if (keyEvent == null)
			return;

		var key = keyEvent.GetKeycodeWithModifiers();
		var step = 15;

		var viewPortRect = GetViewportRect();
		var centerX = viewPortRect.GetCenter();

		if (key == Key.Up)
		{
            Position += new Vector2(0, -step);
        }
		else if (key == Key.Down)
		{
            Position += new Vector2(0, +step);
        }
		else if (key == Key.Right)
		{
            Position += new Vector2(+step, 0);
        }
		else if (key == Key.Left)
		{
            Position += new Vector2(- step, 0);
        }
    }
}