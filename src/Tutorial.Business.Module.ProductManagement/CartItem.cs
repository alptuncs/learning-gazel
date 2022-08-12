using Gazel;
using Gazel.DataAccess;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.Module.ProductManagement
{
    public class CartItem : IGenericInfo, ICartItemInfo
    {
        private IRepository<CartItem> repository;

        protected CartItem() { }
        public CartItem(IRepository<CartItem> repository)
        {
            this.repository = repository;
        }

        public virtual int Id { get; protected set; }
        public virtual Cart Cart { get; protected set; }
        public virtual int Amount { get; protected set; }
        public virtual Product Product { get; protected set; }

        public virtual Money Price => Amount * Product.Price;

        protected internal virtual CartItem With(Cart cart, Product product)
        {
            Cart = cart;
            Product = product;
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

        protected internal virtual void Delete()
        {
            repository.Delete(this);
        }

        string IGenericInfo.Name => Product.Name;
        IGenericInfo ICartItemInfo.Product => Product;
    }

    public class CartItems : Query<CartItem>
    {
        public CartItems(IModuleContext context) : base(context) { }

        internal bool AnyByCart(Cart cart) => AnyBy(t => t.Cart == cart);
        internal List<CartItem> ByCart(Cart cart) => By(t => t.Cart == cart);
        internal CartItem SingleBy(Cart cart, Product product) => SingleBy(t => t.Cart == cart && t.Product == product);

        internal List<CartItem> By(Product product, bool? purchaseComplete = default)
        {
            return By(t => t.Product == product,
                When(purchaseComplete).IsNotDefault().ThenAnd(t => t.Cart.PurchaseComplete == purchaseComplete)
            );
        }

    }
}
