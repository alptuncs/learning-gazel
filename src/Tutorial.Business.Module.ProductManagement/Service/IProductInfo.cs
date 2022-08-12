namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductInfo
    {
        int Id { get; }
        string Name { get; }
        Money Price { get; }
        int Stock { get; }
    }
}
