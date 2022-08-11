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
        IPurchaseRecordInfo GetPurchaseRecord(int purchaseRecordId);
        List<IPurchaseRecordInfo> GetPurchaseRecords(Cart cart);
        List<IPurchaseRecordInfo> GetPurchaseRecords(int lowerBound, int upperBound);
        List<IPurchaseRecordInfo> GetPurchaseRecords(DateTime startDate, DateTime endDate);
    }
}
