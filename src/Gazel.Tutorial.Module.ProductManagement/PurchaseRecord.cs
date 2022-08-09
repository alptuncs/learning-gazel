using Gazel.DataAccess;
using Gazel.Tutorial.Module.ProductManagement.Service;

namespace Gazel.Tutorial.Module.ProductManagement
{

    public class PurchaseRecord : IPurchaseRecordInfo
    {
        private readonly IRepository<PurchaseRecord> repository;
        protected PurchaseRecord() { }

        public PurchaseRecord(IRepository<PurchaseRecord> repository)
        {
            this.repository = repository;
        }

        public virtual int Id { get; protected set; }
        public virtual Cart Cart { get; protected set; }
        public virtual DateTime DateTime { get; protected set; }

        protected internal virtual PurchaseRecord With(Cart cart)
        {
            Cart = cart;
            this.DateTime = DateTime.UtcNow;

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

        IPurchaseRecordInfo IPurchaseRecordsService.GetRecordInfo(PurchaseRecord purchaseRecord) =>
            SingleById(purchaseRecord.Id);

        IPurchaseRecordInfo IPurchaseRecordsService.GetPurchaseRecordsWithCart(Cart cart) =>
            SingleByCart(cart);

        List<IPurchaseRecordInfo> IPurchaseRecordsService.GetPurchaseRecordsWithinTotalCostRange(int lowerBound, int upperBound) =>
            By(lowerBound, upperBound).Cast<IPurchaseRecordInfo>().ToList();

        List<IPurchaseRecordInfo> IPurchaseRecordsService.GetPurchaseRecordsWithingDateTimeRange(DateTime startDate, DateTime endDate) =>
            By(startDate, endDate).Cast<IPurchaseRecordInfo>().ToList();
    }
}