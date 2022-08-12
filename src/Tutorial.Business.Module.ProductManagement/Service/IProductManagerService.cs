namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductManagerService
    {
        IProductInfo CreateProduct(string name, Money price, int stock);
    }
}
