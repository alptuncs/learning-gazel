namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartsService
    {
        ICartDetail GetCart(int cartId);
        List<ICartInfo> GetCarts(string userName);
    }
}
