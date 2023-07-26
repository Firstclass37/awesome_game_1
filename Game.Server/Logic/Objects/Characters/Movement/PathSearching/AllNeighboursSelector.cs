using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class AllNeighboursSelector : INieighborsSearchStrategy<Coordiante>
    {
        private readonly IMapGrid _neighboursAccessor;

        public AllNeighboursSelector(IMapGrid neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public Coordiante[] Search(Coordiante element)
        {
            return _neighboursAccessor.GetNeightborsOf(element).Select(n => n.Key).ToArray();
            //.Where(c => c.CellType == MapCellType.Groud || c.CellType == MapCellType.Road || c.CellType == MapCellType.Resource || c.Tags.Contains(MapCellTags.Trap)).ToArray();
        }
    }
}