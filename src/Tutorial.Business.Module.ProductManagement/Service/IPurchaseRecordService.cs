namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IPurchaseRecordInfo
    {
        int Id { get; }
        Cart Cart { get; }
        DateTime DateTime { get; }
    }

    public interface IPurchaseRecordsService
    {
        IPurchaseRecordInfo RecordInfo(PurchaseRecord purchaseRecord);
        IPurchaseRecordInfo PurchaseRecordsWithCart(Cart cart);
        List<IPurchaseRecordInfo> PurchaseRecordsWithinTotalCostRange(int lowerBound, int upperBound);
        List<IPurchaseRecordInfo> PurchaseRecordsWithingDateTimeRange(DateTime startDate, DateTime endDate);
    }
}
