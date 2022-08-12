using Gazel;
using Gazel.DataAccess;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.Module.ProductManagement
{
    public class Product : IGenericInfo, IProductInfo, IProductService
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
            if (stock < 0) throw new Exception("Stock can't be negative");
            if (price <= 0) throw new Exception("Price can't be negative or 0");
            if (name.IsNullOrWhiteSpace()) throw new Exception("Name can't be empty");
            if (context.Query<Products>().AnyBy(name, true)) throw new Exception("Product is already added");

            Name = name;
            Price = price;
            Stock = stock;
            Available = avalible;

            repository.Insert(this);

            return this;
        }

        public virtual void Update(string name = default)
        {
            if (context.Query<Products>().AnyBy(name, true)) throw new Exception("Product with same name already exists");

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

            Available = false;

            var result = context.New<Product>().With(Name, price, Stock);
            Stock = 0;

            foreach (var item in context.Query<CartItems>().By(this, purchaseComplete: false))
            {
                item.UpdateProduct(result);
            }

            return result;
        }

        public virtual void MakeUnavailable()
        {
            foreach (var item in context.Query<CartItems>().By(this, purchaseComplete: false))
            {
                item.Cart.RemoveProduct(this);
            }

            Available = false;
        }

        #region Service Mappings
        void IProductService.RevisePrice(Money price) => RevisePrice(price);
        void IProductService.MakeUnavailable() => MakeUnavailable();
        void IProductService.Update(string name) => Update(name);
        #endregion
    }

    public class Products : Query<Product>, IProductsService
    {
        public Products(IModuleContext context)
            : base(context) { }

        internal List<Product> ByAvailable(bool available) => By(t => t.Available == available);

        internal bool AnyBy(string name, bool available) => AnyBy(t => t.Name == name && t.Available == available);

        internal List<Product> ByName(string name) => By(t => t.Name == name);

        internal List<Product> ByStock(int min, int max) => By(t => t.Stock > min && t.Stock <= max);

        internal List<Product> ByRange(MoneyRange priceRange) => By(t => t.Price >= priceRange.Start && t.Price <= priceRange.End);

        #region Service Mappings
        IProductInfo IProductsService.GetProduct(int productId) =>
            SingleById(productId);
        List<IProductInfo> IProductsService.GetProducts(bool positiveStock) =>
            ByStock(0, int.MaxValue).Cast<IProductInfo>().ToList();
        List<IProductInfo> IProductsService.GetProducts(MoneyRange range) =>
            ByRange(range).Cast<IProductInfo>().ToList();
        List<IProductInfo> IProductsService.GetProducts(string name) =>
            ByName(name).Cast<IProductInfo>().ToList();
        #endregion
    }
}
