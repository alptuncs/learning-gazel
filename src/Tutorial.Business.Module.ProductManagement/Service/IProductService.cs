using Gazel.DataAccess;

namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductInfo
    {
        int Id { get; }
        string Name { get; }
        Money Price { get; }
        int Stock { get; }
    }

    public interface IGenericInfo
    {
        int Id { get; }
        string Name { get; }
    }

    public interface IProductService
    {
        void Update(string name = default);
        void RevisePrice(Money price);
        void MakeUnavailable();
    }

    public interface IProductsService : IQuery
    {
        // GET /products/{id}
        IProductInfo GetProduct(int productId);
        // GET /product/ProductsWithPositiveStock
        List<IProductInfo> GetProducts(bool positiveStock = false, string name = default, MoneyRange? range = default);
    }

    public interface IProductManagerService
    {
        IProductInfo CreateProduct(string name, Money price, int stock);
    }
}
