namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface IProductManagerService
    {
        Product CreateProduct(string name, float price, int stock);
        void DeleteProduct(int productId);
    }
}
