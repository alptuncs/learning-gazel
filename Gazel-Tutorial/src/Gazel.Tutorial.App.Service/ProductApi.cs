using Castle.MicroKernel;
using Gazel.Configuration;
using Gazel.Tutorial.Module.ProductManagement.Service;
using Gazel.Tutorial.Module.ProductManagement;
using Routine;
using Routine.Engine.Configuration.ConventionBased;
using Castle.MicroKernel;
using Gazel.Configuration;

namespace Gazel.Tutorial.App.Service
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