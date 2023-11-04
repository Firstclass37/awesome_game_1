using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal interface IAllNeighboursSelector : INieighborsSearchStrategy<Coordiante> { }

    internal class AllNeighboursSelector: IAllNeighboursSelector
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