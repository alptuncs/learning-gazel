using Gazel.DataAccess;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{
    public class Product : IProductInfo, IProductService
    {
        private readonly IRepository<Product> repository;
        private readonly IModuleContext context;

        protected Product() { }
        public Product(IRepository<Product> repository, IModuleContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Money Price { get; protected set; }
        public virtual int Stock { get; protected set; }
        public virtual bool Available { get; protected set; }

        protected internal virtual Product With(string name, Money price, int stock, bool avalible = true)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Available = avalible;

            repository.Insert(this);

            return this;
        }

        public virtual void Update(string name = default)
        {
            Name = name ?? Name;
        }

        public virtual void DecreaseStock(int amount)
        {
            if (amount > Stock) throw new Exception("Not enough stock");

            Stock -= amount;
        }

        public virtual Product RevisePrice(Money price)
        {
            if (price.IsDefault()) throw new Exception("price cannot be null");

            var result = context.New<Product>().With(Name, price, Stock);
            Stock = 0;

            foreach (var item in context.Query<CartItems>().By(this, purchaseComplete: false))
            {
                item.UpdateProduct(result);
            }

            return result;
        }

        public virtual void RemoveProduct()
        {
            foreach (var item in context.Query<CartItems>().By(this, purchaseComplete: false))
            {
                item.Cart.RemoveProduct(this);
            }

            Available = false;
        }
    }

    public class Products : Query<Product>, IProductsService
    {
        public Products(IModuleContext context) : base(context) { }

        public List<Product> ByAvailable(bool available)
        {
            return By(t => t.Available == available);
        }

        public List<Product> ByName(string name)
        {
            return By(t => t.Name == name);
        }

        public List<Product> ByPositiveStock()
        {
            return By(t => t.Stock > 0);
        }

        public List<Product> ByPriceRange(MoneyRange priceRange)
        {
            return By(t => t.Price >= priceRange.Start && t.Price <= priceRange.End);
        }

        IProductInfo IProductsService.GetProduct(Product product) =>
            SingleById(product.Id);

        List<IProductInfo> IProductsService.GetProductsWithPositiveStock() =>
            ByPositiveStock().Cast<IProductInfo>().ToList();

        List<IProductInfo> IProductsService.GetProductsWithinPriceRange(MoneyRange range) =>
            ByPriceRange(range).Cast<IProductInfo>().ToList();

        List<IProductInfo> IProductsService.GetProductsWithName(string name) =>
            ByName(name).Cast<IProductInfo>().ToList();
    }
}
