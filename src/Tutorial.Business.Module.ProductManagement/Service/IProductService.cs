namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IProductService
    {
        void Update(string name = default);
        void RevisePrice(Money price);
        void MakeUnavailable();
    }
}
