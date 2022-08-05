﻿using Gazel;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{
    public class ProductManager : IProductManagerService
    {
        private readonly IModuleContext context;

        public ProductManager(IModuleContext context)
        {
            this.context = context;
        }

        public Product CreateProduct(string name, float price, int stock)
        {
            return context.New<Product>().With(name, price, stock);
        }

        public void DeleteProduct(int productId)
        {
            context.Query<Products>().ById(productId).RemoveProduct();
        }
    }
}