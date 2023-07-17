using Godot;

public partial class BuildingsPreview : ColorRect
{
	private HBoxContainer PriceContainer => GetNode<HBoxContainer>("HBoxContainer2");
	private TextureRect Texture => GetNode<TextureRect>("TextureRect");
	private Label Label => GetNode<Label>("Label");


	public Texture2D BuildingTexture { set { Texture.Texture = value; } }

	public string Description { set { Label.Text = value; } }

	public void AddPrices(params Resource[] resource) 
	{
		//clear children before add
		foreach(var resourceItem in resource)
			PriceContainer.AddChild(resourceItem);
	}
}