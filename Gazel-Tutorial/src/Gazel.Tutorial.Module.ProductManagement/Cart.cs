using Gazel.DataAccess;

namespace Gazel.Tutorial.Module.ProductManagement
{

    public class Cart
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
            if (context.Query<CartItems>().SingleByCartAndProduct(this, product) != null)
            {
                context.Query<CartItems>().SingleByCartAndProduct(this, product).UpdateCartItem(amount);
            }
            else
            {
                context.New<CartItem>().With(product, this, amount);
            }

            TotalCost += product.Price * amount;
        }

        public virtual void RemoveFromCart(Product product)
        {
            var cartItem = context.Query<CartItems>().SingleByCartAndProduct(this, product);
            TotalCost -= product.Price * cartItem.Amount;
            cartItem.RemoveCartItem();
        }

        public virtual void EmptyCart()
        {
            context.Query<CartItems>().ByCart(this).ForEach(t => t.RemoveCartItem());
            TotalCost = 0;
        }

        public virtual void DeleteCart()
        {
            repository.Delete(this);
        }

        public virtual List<CartItem> GetCartItems()
        {
            return context.Query<CartItems>().ByCart(this);
        }
    }
    public class Carts : Query<Cart>
    {
        public Carts(IModuleContext context) : base(context) { }

        public Cart SingleByUserName(string userName)
        {
            return SingleBy(t => t.UserName == userName);
        }

        public Cart SingleById(int id)
        {
            return SingleBy(t => t.Id == id);
        }
    }
}

