namespace My_awesome_character.Core.Game.Resources
{
    internal interface IResourceManager
    {
        int GetAmount(int resourceId);
        int[] GetList();
        void Increse(int resourceId, int count);
        bool TrySpend(int resourceId, int count);
    }
}