namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartService
    {
        void AddProduct(Product product, int amount);
        void RemoveProduct(Product product);
        void RemoveAllProducts();
        void Purchase();
    }
}
