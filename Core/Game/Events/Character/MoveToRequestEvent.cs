namespace My_awesome_character.Core.Game.Events.Character
{
    internal class MoveToRequestEvent
    {
        public int CharacterId { get; set; }

        public MapCell TargetCell { get; set; }
    }
}