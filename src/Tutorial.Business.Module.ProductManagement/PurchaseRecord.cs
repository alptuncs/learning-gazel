using Gazel;
using Gazel.DataAccess;
using Tutorial.Business.Module.ProductManagement.Service;

namespace Tutorial.Business.Module.ProductManagement
{
    public class PurchaseRecord : IPurchaseRecordInfo
    {
        private readonly IRepository<PurchaseRecord> repository;
        private readonly ISystem system;

        protected PurchaseRecord() { }

        public PurchaseRecord(IRepository<PurchaseRecord> repository, ISystem system)
        {
            this.repository = repository;
            this.system = system;
        }

        public virtual int Id { get; protected set; }
        public virtual Cart Cart { get; protected set; }
        public virtual DateTime DateTime { get; protected set; }

        protected internal virtual PurchaseRecord With(Cart cart)
        {
            Cart = cart;
            DateTime = system.Now;

            repository.Insert(this);

            return this;
        }
    }

    public class PurchaseRecords : Query<PurchaseRecord>, IPurchaseRecordsService
    {
        public PurchaseRecords(IModuleContext context) : base(context) { }

        public PurchaseRecord ByDateTime(DateTime dateTime)
        {
            return SingleBy(t => t.DateTime == dateTime);
        }

        public List<PurchaseRecord> ByStartDate(DateTime startDate)
        {
            return By(t => t.DateTime >= startDate);
        }

        public List<PurchaseRecord> ByEndDate(DateTime endDate)
        {
            return By(t => t.DateTime <= endDate);
        }

        public List<PurchaseRecord> By(DateTime startDate, DateTime endDate)
        {
            return ByStartDate(startDate).Intersect(ByEndDate(endDate)).ToList();
        }

        public PurchaseRecord SingleByCart(Cart cart)
        {
            return SingleBy(t => t.Cart == cart);
        }

        public List<PurchaseRecord> ByLowerBound(int lowerBound)
        {
            return By(t => t.Cart.TotalCost > lowerBound);
        }

        public List<PurchaseRecord> ByUpperBound(int upperBound)
        {
            return By(t => t.Cart.TotalCost < upperBound);
        }

        public List<PurchaseRecord> By(int lowerBound, int upperBound)
        {
            return ByLowerBound(lowerBound).Intersect(ByUpperBound(upperBound)).ToList();
        }

        IPurchaseRecordInfo IPurchaseRecordsService.RecordInfo(PurchaseRecord purchaseRecord) =>
            SingleById(purchaseRecord.Id);

        IPurchaseRecordInfo IPurchaseRecordsService.PurchaseRecordsWithCart(Cart cart) =>
            SingleByCart(cart);

        List<IPurchaseRecordInfo> IPurchaseRecordsService.PurchaseRecordsWithinTotalCostRange(int lowerBound, int upperBound) =>
            By(lowerBound, upperBound).Cast<IPurchaseRecordInfo>().ToList();

        List<IPurchaseRecordInfo> IPurchaseRecordsService.PurchaseRecordsWithingDateTimeRange(DateTime startDate, DateTime endDate) =>
            By(startDate, endDate).Cast<IPurchaseRecordInfo>().ToList();
    }
}
