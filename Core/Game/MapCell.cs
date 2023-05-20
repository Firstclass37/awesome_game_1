using My_awesome_character.Core.Constatns;
using System.Diagnostics.CodeAnalysis;

public struct MapCell
{
	public MapCell(int x, int y, MapCellType cellType,  params string[] tags)
	{
		X = x;
		Y = y;
		Tags = tags;
		CellType = cellType;
	}

	public int X { get; set; }

	public int Y { get; set; }

	public MapCellType CellType { get; set; }

	public string[] Tags { get; set; }

    public static bool operator !=(MapCell a, MapCell b) => !(a == b);

    public static bool operator ==(MapCell a, MapCell b) => a.X == b.X && a.Y == b.Y;

	public override string ToString() => $"\"{X} {Y}\"";

	public override bool Equals([NotNullWhen(true)] object obj) => this == (MapCell)obj;

	public override int GetHashCode() => $"{X} {Y}".GetHashCode();
}