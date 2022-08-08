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
        public virtual float TotalCost { get; protected set; }

        protected internal virtual Cart With(string userName)
        {
            UserName = userName;
            TotalCost = 0;

            repository.Insert(this);

            return this;
        }

        public virtual void AddToCart(Product product, int amount)
        {
            if (amount > product.Stock) throw new Exception($"There are {product.Stock} amount of products in stock, can't add {amount} amount to your cart");

            if (context.Query<CartItems>().SingleBy(this, product) != null)
            {
                var cartItem = context.Query<CartItems>().SingleBy(this, product);
                cartItem.UpdateCartItem(cartItem.Amount + amount);
            }
            else
            {
                context.New<CartItem>().With(product, this, amount);
            }

            TotalCost += product.Price * amount;
        }

        public virtual void AddToCart(Product product)
        {
            if (product.Stock == 0) throw new Exception($"There are {product.Stock} amount of products in stock, can't add product to your cart");

            if (context.Query<CartItems>().SingleBy(this, product) != null)
            {
                var cartItem = context.Query<CartItems>().SingleBy(this, product);
                cartItem.UpdateCartItem(cartItem.Amount + 1);
            }
            else
            {
                context.New<CartItem>().With(product, this, 1);
            }

            TotalCost += product.Price;
        }

        public virtual void RemoveFromCart(Product product)
        {
            var cartItem = context.Query<CartItems>().SingleBy(this, product);
            TotalCost -= product.Price * cartItem.Amount;
            cartItem.RemoveCartItem();
        }

        public virtual void RemoveAllProducts()
        {
            context.Query<CartItems>().ByCart(this).ForEach(t => t.RemoveCartItem());
            TotalCost = 0;
        }

        public virtual void DeleteCart()
        {
            RemoveAllProducts();
            repository.Delete(this);
        }

        public virtual List<CartItem> GetCartItems()
        {
            return context.Query<CartItems>().ByCart(this);
        }

        public virtual PurchaseRecord CompletePurchase()
        {
            if (context.Query<CartItems>().ByCart(this) == null) throw new Exception("Cart is empty");

            var purchaseRecord = context.New<PurchaseRecord>().With(this);

            UpdateProductStock(context.Query<CartItems>().ByCart(this));
            TotalCost = 0;

            return purchaseRecord;
        }

        public virtual void UpdateProductStock(List<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                item.Product.UpdateProduct(null, default(float), item.Product.Stock - item.Amount);
                item.RemoveCartItem();
            }
        }

        public virtual List<PurchaseRecord> GetPurchaseRecords()
        {
            return context.Query<PurchaseRecords>().ByCart(this);
        }
    }
    public class Carts : Query<Cart>, ICartsService
    {
        public Carts(IModuleContext context) : base(context) { }

        public Cart SingleByUserName(string userName)
        {
            return SingleBy(t => t.UserName == userName);
        }

        public List<Cart> ByNotEmpty()
        {
            return By(t => t.TotalCost > 0);
        }

        ICartInfo ICartsService.GetCart(Cart cart) =>
            SingleById(cart.Id);

        ICartInfo ICartsService.GetCartWithName(string name) =>
            SingleByUserName(name);

        List<ICartInfo> ICartsService.GetNonEmptyCarts() =>
            ByNotEmpty().Cast<ICartInfo>().ToList();
    }
}

