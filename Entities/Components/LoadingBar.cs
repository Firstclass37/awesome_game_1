using Godot;
using System;

public partial class LoadingBar : Node2D
{
	private ColorRect ProgressRect => GetNode<ColorRect>("ColorRect");
	private ColorRect BackgroundProgressRect => GetNode<ColorRect>("HBoxContainer/ColorRect");
    private Label CounterLabel => GetNode<Label>("Label");

    private double TileLeft
	{
		get { return double.Parse(CounterLabel.Text); }
		set { CounterLabel.Text = ((int)value).ToString(); }
	}

    public double Duration { get; set; }

    public void Start(Action onEnd)
	{
		CounterLabel.Text = Duration.ToString("##");

		var tween = CreateTween();
		tween.SetParallel();
		tween.TweenProperty(ProgressRect, "size", new Vector2(BackgroundProgressRect.Size.X, ProgressRect.Size.Y), Duration);
		tween.TweenMethod(Callable.From<double>(v => { TileLeft = v; }), Duration, 1, Duration);
		tween.Chain()
			.TweenCallback(Callable.From(() => { onEnd?.Invoke(); }));

		tween.Play();
	}
}