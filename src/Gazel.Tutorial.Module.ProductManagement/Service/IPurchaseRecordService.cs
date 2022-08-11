namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface IPurchaseRecordInfo
    {
        int Id { get; }
        Cart Cart { get; }
        DateTime DateTime { get; }
    }

    public interface IPurchaseRecordsService
    {
        IPurchaseRecordInfo GetRecordInfo(PurchaseRecord purchaseRecord);
        IPurchaseRecordInfo GetPurchaseRecordsWithCart(Cart cart);
        List<IPurchaseRecordInfo> GetPurchaseRecordsWithinTotalCostRange(int lowerBound, int upperBound);
        List<IPurchaseRecordInfo> GetPurchaseRecordsWithingDateTimeRange(DateTime startDate, DateTime endDate);
    }
}
