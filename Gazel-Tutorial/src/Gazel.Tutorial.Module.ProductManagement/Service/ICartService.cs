namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface ICartService
    {
        int Id { get; }
        List<CartItem> CartProducts { get; }
        string UserName { get; }
        float TotalCost { get; }
    }

    public interface ICartsService
    {

    }
}
