using Game.Server.Models.Resources;

namespace Game.Server.Logic.Resources
{
    internal interface IResourceManager
    {
        void AddResource(int resourceId, int initialValue);

        int GetAmount(int resourceId);
        Resource Get(int resourceId);
        void Increase(int resourceId, int count);
        bool TrySpend(int resourceId, int count);
    }
}