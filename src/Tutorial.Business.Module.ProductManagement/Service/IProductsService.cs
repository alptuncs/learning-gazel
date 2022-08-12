namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductsService
    {
        IProductInfo GetProduct(int productId);
        List<IProductInfo> GetProducts(bool positiveStock);
        List<IProductInfo> GetProducts(string name);
        List<IProductInfo> GetProducts(MoneyRange range);
    }
}
