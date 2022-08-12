namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IPurchaseRecordInfo
    {
        int Id { get; }
        ICartDetail Cart { get; }
        DateTime DateTime { get; }
    }
}
