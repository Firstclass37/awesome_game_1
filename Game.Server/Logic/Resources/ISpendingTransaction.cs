using Game.Server.Models.Resources;

namespace Game.Server.Logic.Resources
{
    internal interface ISpendingTransactionFactory
    {
        ISpendingTransaction Create();
    }

    internal class SpendingTransactionFactory : ISpendingTransactionFactory
    {
        private readonly IResourceManager _resourceManager;

        public SpendingTransactionFactory(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public ISpendingTransaction Create() => new SpendingTransaction(_resourceManager);
    }

    internal interface ISpendingTransaction: IDisposable
    {
        void Spend(IReadOnlyCollection<ResourceChunk> price);

        void Commit();
    }

    internal class SpendingTransaction : ISpendingTransaction
    {
        private readonly IResourceManager _resourceManager;
        private bool _commited = false;
        private readonly List<ResourceChunk> _spentResources = new();

        public SpendingTransaction(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void Spend(IReadOnlyCollection<ResourceChunk> prices)
        {
            try 
            {
                foreach (var price in prices)
                {
                    if (!_resourceManager.TrySpend(price.ResourceId, price.Amout)) 
                        throw new ArgumentException($"not enough resources");

                    _spentResources.Add(price);
                }
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Commit()
        {
            _commited = true;
        }

        public void Dispose()
        {
            if (!_commited)
                Rollback();
        }

        private void Rollback()
        {
            foreach (var price in _spentResources)
                _resourceManager.Increase(price.ResourceId, price.Amout);
        }
    }
}