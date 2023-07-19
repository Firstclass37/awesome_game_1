namespace Game.Server.Logic
{
    internal interface IGameCycle
    {
        void Tick(double gameTimeMs);
    }
}