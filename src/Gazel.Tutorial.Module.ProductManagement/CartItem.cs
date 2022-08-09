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
        public virtual int Amount { get; protected set; }
        public virtual Product Product { get; protected set; }
        public virtual Cart Cart { get; protected set; }

        protected internal virtual CartItem With(Product product, Cart cart)
        {
            Product = product;
            Cart = cart;
            Amount = 0;

            repository.Insert(this);

            return this;
        }

        protected internal virtual void UpdateProduct(Product product)
        {
            Product = product;
        }

        protected internal virtual void IncreaseAmount(int amount)
        {
            Amount += amount;
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

        public List<CartItem> By(Product product, bool? purchaseComplete = default)
        {
            return By(t => t.Product == product,
                When(purchaseComplete).IsNotDefault().ThenAnd(t => t.Cart.PurchaseComplete == purchaseComplete)
            );
        }

        public CartItem SingleBy(Cart cart, Product product)
        {
            return SingleBy(t => t.Cart == cart && t.Product == product);
        }
    }
}
