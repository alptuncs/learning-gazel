namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductsService
    {
        IProductInfo GetProduct(int productId);
        List<IProductInfo> GetProducts(bool positiveStock = false, string name = default, MoneyRange? range = default);
    }
}
