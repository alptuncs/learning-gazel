using Gazel.DataAccess;
using Gazel.Tutorial.Module.ProductManagement.Service;

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
        public virtual int Amount { get; protected set; }
        public virtual Product Product { get; protected set; }
        public virtual Cart Cart { get; protected set; }

        protected internal virtual CartItem With(Product product, Cart cart, int amount)
        {
            Product = product;
            Cart = cart;
            Amount = amount;
            repository.Insert(this);

            return this;
        }

        public virtual void UpdateCartItem(int amount)
        {
            Amount = amount;
        }

        public virtual void RemoveCartItem()
        {

            repository.Delete(this);
        }
    }

    public class CartItems : Query<CartItem>
    {
        public CartItems(IModuleContext context) : base(context) { }

        public List<CartItem> ByCart(Cart cart)
        {
            return By(t => t.Cart == cart);
        }

        public List<CartItem> ByProduct(Product product)
        {
            return By(t => t.Product.Id == product.Id);
        }

        public CartItem SingleBy(Cart cart, Product product)
        {
            return SingleBy(t => t.Cart == cart && t.Product == product);
        }
    }
}
