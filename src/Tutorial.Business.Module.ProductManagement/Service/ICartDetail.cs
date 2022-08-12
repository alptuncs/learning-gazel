namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartDetail
    {
        int Id { get; }
        string UserName { get; }
        Money TotalCost { get; }
        bool PurchaseComplete { get; }
        List<ICartItemInfo> Items { get; }
    }
}
