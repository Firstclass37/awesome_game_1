using Godot;
using System.Linq;

public partial class BuildingCollection : ScrollContainer
{
	private Container BuidlingContainer => GetNode<Container>("BuildingsContainer");

	public void AddBuilding(BuildingsPreview buildingsPreview)
	{
		BuidlingContainer.AddChild(buildingsPreview);
	}

	public BuildingsPreview[] GetList() => BuidlingContainer.GetChildren().Cast<BuildingsPreview>().ToArray();

}
