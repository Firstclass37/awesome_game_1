using Game.Server.Models.Constants;

namespace Game.Server.Models.Maps
{
    internal record IsometricMapCell: IEntityObject
    {
        public IsometricMapCell(Coordiante coordiante)
        {
            Id = Guid.NewGuid();
            Coordiante = coordiante;
            Neighbors = new();
        }

        public IsometricMapCell(Coordiante coordiante, Dictionary<IsometricMapCell, Direction> neighbors)
        {
            Id = Guid.NewGuid();
            Coordiante = coordiante;
            Neighbors = neighbors;
        }

        public Guid Id { get; set; }

        public Coordiante Coordiante { get; }

        public Dictionary<IsometricMapCell, Direction> Neighbors { get; init; }
    }
}