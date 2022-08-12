namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IPurchaseRecordsService
    {
        IPurchaseRecordInfo GetPurchaseRecord(int purchaseRecordId);
        List<IPurchaseRecordInfo> GetPurchaseRecords(Cart cart);
        List<IPurchaseRecordInfo> GetPurchaseRecords(MoneyRange range);
        List<IPurchaseRecordInfo> GetPurchaseRecords(DateTime startDate, DateTime endDate);
    }
}
