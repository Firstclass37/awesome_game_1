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

        public Coordiante Coordiante { get; set; }

        public Dictionary<IsometricMapCell, Direction> Neighbors { get; set; }
    }
}