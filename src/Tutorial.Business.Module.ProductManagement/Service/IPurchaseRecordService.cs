﻿namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface IPurchaseRecordInfo
    {
        int Id { get; }
        Cart Cart { get; }
        DateTime DateTime { get; }
    }
}
