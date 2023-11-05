using Godot;
using My_awesome_character.Core.Game;
using System;

public partial class Projectile : Node2D
{
    private Tween _movingTween;
    private TextureRect TextureRect => GetNode<TextureRect>("TextureRect");

    public Guid Id { get; set; }

    public CoordianteUI MapPosition { get; set; }

    public void SetTexture(Texture2D texture)
    {
        TextureRect.Texture = texture;
    }

    public void MoveTo(CoordianteUI to, float speed, Func<CoordianteUI, Vector2> positionProvider)
    {
        if (_movingTween != null)
            _movingTween.Kill();

        _movingTween = CreateTween();

        var targetPosition = positionProvider(to);
        _movingTween.TweenProperty(this, "position", targetPosition, speed);

        _movingTween.Play();
    }
}