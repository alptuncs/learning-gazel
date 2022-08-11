using Castle.MicroKernel;
using Gazel.Configuration;
using Routine;
using Routine.Engine.Configuration.ConventionBased;
using Tutorial.Business.Module.ProductManagement;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.App.Service.ApiPackages
{
    public class CartService : ICodingStyleConfiguration
    {
        public void Configure(ConventionBasedCodingStyle codingStyle, IKernel kernel)
        {
            codingStyle.AddTypes(v => v.ApiPackage("Cart", t => t
                .Methods.Add(c => c.Proxy<ICartService>().TargetByParameter<Cart>())
                .Methods.Add(c => c.Proxy<ICartsService>().TargetBySingleton(kernel))
                .Methods.Add(c => c.Proxy<ICartManagerService>().TargetBySingleton(kernel))
            ));
        }
    }
}