using Godot;

public partial class Camera2D : Godot.Camera2D
{
    private readonly Vector2 _minZoom = new Vector2(0.3f, 0.3f);
    private readonly Vector2 _maxZoom = new Vector2(2.5f, 2.5f);

    private const float _zoomStep = 0.01f;

    public override void _Process(double delta)
    {
        if (Input.IsPhysicalKeyPressed(Key.Minus) && Zoom > _minZoom)
        {
            Zoom -= new Vector2(_zoomStep, _zoomStep);
        }
        else if (Input.IsPhysicalKeyPressed(Key.Equal) && Zoom < _maxZoom)
        {
            Zoom += new Vector2(_zoomStep, _zoomStep);
        }
    }
}