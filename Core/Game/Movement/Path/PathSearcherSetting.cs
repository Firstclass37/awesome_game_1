namespace My_awesome_character.Core.Game.Movement.Path
{
    internal class PathSearcherSetting<T>
    {
        public IGScoreStrategy<T> GScoreStrategy { get; set; }
        public IFScoreStrategy<T> FScoreStrategy { get; set; }
        public INieighborsSearchStrategy<T> NeighborsSearchStrategy { get; set; }
    }
}