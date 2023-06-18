namespace My_awesome_character.Core.Game.Movement
{
    internal interface ICharacterMovement
    {
        void MoveTo(character character, Coordiante coordiante);
        void StopMoving(character character);
    }
}