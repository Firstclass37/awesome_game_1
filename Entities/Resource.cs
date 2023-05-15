using Godot;

public partial class Resource : HBoxContainer
{
	private Label AmountLabel => GetNode<Label>("HBoxContainer/Label");
	private TextureRect TextureRect => GetNode<TextureRect>("HBoxContainer/TextureRect");

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public Texture2D PreviewTexture 
	{
		set { TextureRect.Texture = value; }
	}

	public int Amount
	{
		get { return int.Parse(AmountLabel.Text); }
		set { AmountLabel.Text = value.ToString(); }
	}
}