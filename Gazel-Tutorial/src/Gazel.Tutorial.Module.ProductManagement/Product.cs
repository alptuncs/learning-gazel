using Gazel;
using Gazel.DataAccess;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{

    public class Product : IProductInfo, IProductService
    {
        private IRepository<Product> repository;
        private readonly IModuleContext context;

        protected Product() { }

        public Product(IRepository<Product> repository, IModuleContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public virtual int Id { get; protected set; }
        public virtual string ProductName { get; protected set; }
        public virtual float Price { get; protected set; }
        public virtual int Stock { get; protected set; }

        protected internal virtual Product With(string name, float price, int stock)
        {
            ProductName = name;
            Price = price;
            Stock = stock;

            repository.Insert(this);

            return this;
        }
        public virtual void UpdateProduct(string name = null, float price = default(float), int stock = default(int))
        {
            if (name.IsNullOrWhiteSpace()) name = ProductName;
            if (price.IsDefault()) price = Price;
            if (stock.IsDefault()) stock = Stock;

            ProductName = name;
            Price = price;
            Stock = stock;
        }

        public virtual void RemoveProduct()
        {
            context.Query<CartItems>().ByProduct(this).ForEach(t => t.Cart.RemoveFromCart(this));
            repository.Delete(this);
        }
    }

    public class Products : Query<Product>, IProductsService
    {
        public Products(IModuleContext context) : base(context) { }

        public List<Product> ByName(string name)
        {
            return By(t => t.ProductName == name);
        }

        public List<Product> ByPositiveStock()
        {
            return By(t => t.Stock > 0);
        }

        public List<Product> ByLowerBound(float lowerBound)
        {
            return By(t => t.Price >= lowerBound);
        }

        public List<Product> ByUpperBound(float upperBound)
        {
            return By(t => t.Price <= upperBound);
        }

        public List<Product> By(float lowerBound, float upperBound)
        {
            return ByLowerBound(lowerBound).Intersect(ByUpperBound(upperBound)).ToList();
        }

        IProductInfo IProductsService.GetProduct(Product product) =>
            SingleById(product.Id);

        List<IProductInfo> IProductsService.GetProductsWithPositiveStock() =>
            ByPositiveStock().Cast<IProductInfo>().ToList();

        List<IProductInfo> IProductsService.GetProductsWithinPriceRange(float lowerBound, float upperBound) =>
            By(lowerBound, upperBound).Cast<IProductInfo>().ToList();

        List<IProductInfo> IProductsService.GetProductsWithName(string name) =>
            ByName(name).Cast<IProductInfo>().ToList();
    }
}
