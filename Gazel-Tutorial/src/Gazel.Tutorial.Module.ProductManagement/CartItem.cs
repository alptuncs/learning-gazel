using Gazel.DataAccess;

namespace Gazel.Tutorial.Module.ProductManagement
{
    public class CartItem
    {
        private IRepository<CartItem> repository;

        protected CartItem() { }

        public CartItem(IRepository<CartItem> repository)
        {
            this.repository = repository;
        }

        public virtual int Id { get; protected set; }
        public virtual int CartId { get; protected set; }
        public virtual int Amount { get; protected set; }
        public virtual Product ProductInfo { get; protected set; }
        protected internal virtual CartItem With(Product product, int cartId, int amount)
        {
            ProductInfo = product;
            CartId = cartId;
            Amount = amount;
            repository.Insert(this);

            return this;
        }
    }
}
