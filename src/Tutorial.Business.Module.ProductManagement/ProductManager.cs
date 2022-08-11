﻿using Gazel;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.Module.ProductManagement
{
    public class ProductManager : IProductManagerService
    {
        private readonly IModuleContext context;

        public ProductManager(IModuleContext context)
        {
            this.context = context;
        }

        public Product CreateProduct(string name, Money price, int stock)
        {
            return context.New<Product>().With(name, price, stock);
        }

        public Cart CreateCart(string userName)
        {
            return context.New<Cart>().With(userName);
        }
    }
}