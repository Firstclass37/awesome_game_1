namespace Game.Server.Models.Weapons
{
    //Todo: PrepareSpeed - сколько время надо потратить на приготовление к атаке (например, замахнуться, или прицелиться и т.п.)
    internal record Weapon(string Name, double Damage, int Distance, double Speed, string DamageType, double? PrepareSpeed = null);
}