namespace My_awesome_character.Core.Game.Movement.Path
{
    internal class PathSearcherSetting<T>
    {
        public IGScoreStrategy<T> GScoreStrategy { get; set; }
        public IHScoreStrategy<T> HScoreStrategy { get; set; }
        public INieighborsSearchStrategy<T> NeighborsSearchStrategy { get; set; }
    }
}