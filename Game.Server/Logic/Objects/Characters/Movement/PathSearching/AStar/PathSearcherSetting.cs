namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar
{
    internal class PathSearcherSetting<T>
    {
        public IGScoreStrategy<T> GScoreStrategy { get; set; }
        public IHScoreStrategy<T> HScoreStrategy { get; set; }
        public INieighborsSearchStrategy<T> NeighborsSearchStrategy { get; set; }
    }
}