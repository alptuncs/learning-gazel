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
        void UpdateProductName(string productName);
        void UpdateProductPrice(float price);
        void UpdateProductStock(int stock);
    }

    public interface IProductsService
    {
        IProductInfo GetProduct(int productId);
        List<IProductInfo> GetProductsWithPositiveStock();
        List<IProductInfo> GetProductsWithName(string name);
        List<IProductInfo> GetProductsWithinPriceRange(float lowerBound, float upperBound);
    }
}
