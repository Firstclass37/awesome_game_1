namespace Game.Server.Logic.Characters.Movement.PathSearching.Base
{
    internal class PathSearcherSetting<T>
    {
        public IGScoreStrategy<T> GScoreStrategy { get; set; }
        public IHScoreStrategy<T> HScoreStrategy { get; set; }
        public INieighborsSearchStrategy<T> NeighborsSearchStrategy { get; set; }
    }
}