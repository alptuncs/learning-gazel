using Castle.MicroKernel;
using Gazel.Configuration;
using Routine;
using Routine.Engine.Configuration.ConventionBased;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.App.Service
{
    public class ProductAPi : ICodingStyleConfiguration
    {
        public void Configure(ConventionBasedCodingStyle codingStyle, IKernel kernel)
        {
            codingStyle.AddTypes(v => v.ApiPackage("Product", t => t
                .Methods.Add(c => c.Proxy<IProductService>().TargetByParameter<Module.ProductManagement.Product>())
                .Methods.Add(c => c.Proxy<IProductsService>().TargetBySingleton(kernel))
                .Methods.Add(c => c.Proxy<ICartService>().TargetByParameter<Module.ProductManagement.Cart>())
                .Methods.Add(c => c.Proxy<ICartsService>().TargetBySingleton(kernel))
                .Methods.Add(c => c.Proxy<IPurchaseRecordsService>().TargetBySingleton(kernel))
                .Methods.Add(c => c.Proxy<IProductManagerService>().TargetBySingleton(kernel))
            ));
        }
    }
}