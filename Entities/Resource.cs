using Godot;

public partial class Resource : HBoxContainer
{
	private Label AmountLabel => GetNode<Label>("Label");
	private TextureRect TextureRect => GetNode<TextureRect>("TextureRect");

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public int ResourceType { get; set; }

	public Texture2D PreviewTexture 
	{
		set { TextureRect.Texture = value; }
	}

	public int Amount
	{
		get { return int.Parse(AmountLabel.Text); }
		set { AmountLabel.Text = value.ToString(); }
	}

	public string Description { get; set; }
}
