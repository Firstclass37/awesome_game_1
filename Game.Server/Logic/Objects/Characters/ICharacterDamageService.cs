using Game.Server.Models.GamesObjectList;

namespace Game.Server.Logic.Objects.Characters
{
    internal interface ICharacterDamageService
    {
        void Damage(Character character, double damage);
        void InstantKill(Character character);
    }

    internal enum DamageResult
    {
        Completed,
        Rejected
    }
}