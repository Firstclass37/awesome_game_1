using Godot;

public partial class CameraAnchor : ColorRect
{
    private const int _step = 5;

    public override void _Process(double delta)
    {
        if (Input.IsPhysicalKeyPressed(Key.Up))
        {
            Position += new Vector2(0, -_step);
        }
        if (Input.IsPhysicalKeyPressed(Key.Down))
        {
            Position += new Vector2(0, +_step);
        }
        if (Input.IsPhysicalKeyPressed(Key.Right))
        {
            Position += new Vector2(+_step, 0);
        }
        if (Input.IsPhysicalKeyPressed(Key.Left))
        {
            Position += new Vector2(-_step, 0);
        }
    }
}