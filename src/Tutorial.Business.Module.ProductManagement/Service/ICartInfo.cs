namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartInfo
    {
        int Id { get; }
        string UserName { get; }
        Money TotalCost { get; }
        bool PurchaseComplete { get; }
    }
}
