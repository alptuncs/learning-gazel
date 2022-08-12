using Castle.MicroKernel;
using Gazel.Configuration;
using Routine;
using Routine.Engine.Configuration.ConventionBased;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.App.Service.ApiPackages
{
    public class PurchaseService : ICodingStyleConfiguration
    {
        public void Configure(ConventionBasedCodingStyle codingStyle, IKernel kernel)
        {
            codingStyle.AddTypes(v => v.ApiPackage("PurchaseRecord", t => t
                .Methods.Add(c => c.Proxy<IPurchaseRecordsService>().TargetBySingleton(kernel))
            ));
        }
    }
}