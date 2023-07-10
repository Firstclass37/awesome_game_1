using Game.Server.Models.Resources;

namespace Game.Server.Logic.Resources
{
    internal interface IResourceManager
    {
        int GetAmount(int resourceType);
        IReadOnlyCollection<Resource> GetList();
        void Increase(int resourceType, int count);
        bool TrySpend(int resourceType, int count);
    }
}