namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface IProductInfo
    {
        int Id { get; }
        string ProductName { get; }
        float Price { get; }
        int Stock { get; }
    }

    public interface IProductService
    {
        Product UpdateProductInfo(string name = null, float price = default(float), int stock = default(int));
        void RemoveProduct();
    }

    public interface IProductsService
    {
        IProductInfo GetProduct(Product product);
        List<IProductInfo> GetProductsWithPositiveStock();
        List<IProductInfo> GetProductsWithName(string name);
        List<IProductInfo> GetProductsWithinPriceRange(float lowerBound, float upperBound);
    }

    public interface IProductManagerService
    {
        Product CreateProduct(string name, float price, int stock);
        Cart CreateCart(string userName);
    }
}
