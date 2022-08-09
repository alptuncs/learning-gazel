using Gazel.DataAccess;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{
    public class Cart : ICartInfo, ICartService
    {
        private readonly IRepository<Cart> repository;
        private readonly IModuleContext context;

        protected Cart() { }

        public Cart(IRepository<Cart> repository, IModuleContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public virtual int Id { get; protected set; }
        public virtual string UserName { get; protected set; }
        public virtual Money TotalCost { get; protected set; }
        public virtual bool PurchaseComplete { get; protected set; }

        protected internal virtual Cart With(string userName, bool purchaseComplete = false)
        {
            UserName = userName;
            PurchaseComplete = purchaseComplete;
            TotalCost = default;

            repository.Insert(this);

            return this;
        }

        public virtual void AddToCart(Product product, int amount = 1)
        {
            if (PurchaseComplete) throw new Exception("Cannot modify cart after being purchased");

            var item = context.Query<CartItems>().SingleBy(this, product) ??
                       context.New<CartItem>().With(product, this);

            item.IncreaseAmount(amount);

            TotalCost += item.Price;
        }

        public virtual void RemoveFromCart(Product product)
        {
            if (PurchaseComplete) throw new Exception("Cannot modify cart after being purchased");

            var item = context.Query<CartItems>().SingleBy(this, product);
            if (item == null) throw new Exception("Product not found in cart");

            TotalCost -= item.Price;
            item.Delete();
        }

        public virtual void RemoveAllProducts()
        {
            if (PurchaseComplete) throw new Exception("Cannot modify cart after being purchased");

            foreach (var item in context.Query<CartItems>().ByCart(this))
            {
                item.Delete();
            }

            TotalCost = default;
        }

        public virtual List<CartItem> GetCartItems()
        {
            return context.Query<CartItems>().ByCart(this);
        }

        public virtual PurchaseRecord CompletePurchase()
        {
            if (PurchaseComplete) throw new Exception("Purchase could not be completed. This cart has already completed a purchase before");
            if (!context.Query<CartItems>().AnyByCart(this)) throw new Exception("Cart is empty");

            foreach (var item in context.Query<CartItems>().ByCart(this))
            {
                item.Product.DecreaseStock(item.Amount);
            }

            PurchaseComplete = true;

            return context.New<PurchaseRecord>().With(this);
        }

        public virtual PurchaseRecord GetPurchaseRecord()
        {
            return context.Query<PurchaseRecords>().SingleByCart(this);
        }
    }

    public class Carts : Query<Cart>, ICartsService
    {
        public Carts(IModuleContext context) : base(context) { }

        public Cart SingleByUserName(string userName)
        {
            return SingleBy(t => t.UserName == userName);
        }

        public List<Cart> NotEmpty()
        {
            return By(t => t.TotalCost > 0);
        }

        ICartInfo ICartsService.GetCart(Cart cart) =>
            SingleById(cart.Id);

        ICartInfo ICartsService.GetCartWithName(string name) =>
            SingleByUserName(name);

        List<ICartInfo> ICartsService.GetNonEmptyCarts() =>
            NotEmpty().Cast<ICartInfo>().ToList();
    }
}
