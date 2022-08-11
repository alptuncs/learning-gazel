﻿using Gazel.DataAccess;
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
            if (userName.IsNullOrWhiteSpace()) throw new Exception("User Name can't be empty");

            UserName = userName;
            PurchaseComplete = purchaseComplete;
            TotalCost = default;

            repository.Insert(this);

            return this;
        }

        public virtual List<CartItem> GetCartItems() => context.Query<CartItems>().ByCart(this);

        protected virtual void ValidateNotPurchased()
        {
            if (PurchaseComplete) throw new Exception("Cannot modify cart after being purchased");
        }

        public virtual void AddProduct(Product product, int amount = 1)
        {
            ValidateNotPurchased();

            var item = context.Query<CartItems>().SingleBy(this, product) ??
                       context.New<CartItem>().With(this, product);

            item.IncreaseAmount(amount);
        }

        public virtual void RemoveProduct(Product product)
        {
            ValidateNotPurchased();

            var item = context.Query<CartItems>().SingleBy(this, product) ??
                       throw new Exception("Product not found in cart");

            item.Delete();
        }

        public virtual void RemoveAllProducts()
        {
            ValidateNotPurchased();

            foreach (var item in context.Query<CartItems>().ByCart(this))
            {
                item.Delete();
            }
        }

        public virtual PurchaseRecord Purchase()
        {
            ValidateNotPurchased();

            if (!context.Query<CartItems>().AnyByCart(this)) throw new Exception("Cart is empty");

            foreach (var item in GetCartItems())
            {
                TotalCost += item.Price;
                item.Product.DecreaseStock(item.Amount);
            }

            PurchaseComplete = true;

            return context.New<PurchaseRecord>().With(this);
        }

        public virtual PurchaseRecord GetPurchaseRecord() => context.Query<PurchaseRecords>().SingleByCart(this);
    }

    public class Carts : Query<Cart>, ICartsService
    {
        public Carts(IModuleContext context) : base(context) { }

        internal Cart SingleByUserName(string userName) => SingleBy(t => t.UserName == userName);
        internal List<Cart> NotEmpty() => By(t => t.TotalCost > 0);

        ICartInfo ICartsService.GetCart(Cart cart) => SingleById(cart.Id);
        ICartInfo ICartsService.GetCartWithName(string name) => SingleByUserName(name);
        List<ICartInfo> ICartsService.GetNonEmptyCarts() => NotEmpty().Cast<ICartInfo>().ToList();
    }
}
