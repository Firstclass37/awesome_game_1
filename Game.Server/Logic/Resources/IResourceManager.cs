using Game.Server.Models.Resources;

namespace Game.Server.Logic.Resources
{
    internal interface IResourceManager
    {
        float GetAmount(int resourceType);
        IReadOnlyCollection<Resource> GetList();
        void Increase(int resourceType, float count);
        bool TrySpend(int resourceType, float count);
    }
}