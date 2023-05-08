namespace My_awesome_character.Core.Systems
{
    internal interface ISystem
    {
        void OnStart();

        void Process(double gameTime);
    }
}