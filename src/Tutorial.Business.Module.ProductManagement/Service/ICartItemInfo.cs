namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartItemInfo
    {
        int Id { get; }
        int Amount { get; }
        IGenericInfo Product { get; }
    }
}
