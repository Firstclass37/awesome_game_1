namespace Game.Server.API.GameInit
{
    public interface IGameController
    {
        void StartNewGame();
        void Tick(double gameTime);
    }
}