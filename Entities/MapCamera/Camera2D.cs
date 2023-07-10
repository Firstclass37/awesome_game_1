using Godot;
using System;

public partial class Camera2D : Godot.Camera2D
{
    private readonly Vector2 _minZoom = new Vector2(0.6f, 0.6f);
    private readonly Vector2 _maxZoom = new Vector2(2.5f, 2.5f);

    public override void _Input(InputEvent @event)
    {
        var keyEvent = @event as InputEventKey;
        if (keyEvent == null)
            return;

        var key = keyEvent.GetKeycodeWithModifiers();

        var zoomStep = 0.1F;
        
        if (key == Key.Minus && Zoom > _minZoom)
        {
            Zoom -= new Vector2(zoomStep, zoomStep);
        }
        else if (key == Key.Equal && Zoom < _maxZoom)
        {
            Zoom += new Vector2(zoomStep, zoomStep);
        }
    }
}