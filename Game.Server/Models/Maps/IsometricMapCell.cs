using Game.Server.Models.Constants;

namespace Game.Server.Models.Maps
{
    internal class IsometricMapCell: IEntityObject
    {
        public IsometricMapCell()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Dictionary<Direction, IsometricMapCell> Neighbors { get; set; }
    }
}