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

    public interface IProductService
    {
        void RevisePrice(Money price);
        void MakeUnavailable();
    }

    public interface IProductsService : IQuery
    {
        // GET /products/{id}
        IProductInfo GetProduct(int productId);
        // GET /product/ProductsWithPositiveStock
        List<IProductInfo> GetProducts(bool positiveStock = false, string name = default, MoneyRange range = default);
    }

    public interface IProductManagerService
    {
        Product CreateProduct(string name, Money price, int stock);
        Cart CreateCart(string userName);
    }
}
