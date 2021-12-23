namespace WebStore.Domain.Types
{
    public enum OrderStatusType
    {
        AwaitingProcessing,
        AwaitingPayment,

        SendingExpected,
        Packing,
        WaitingSent,

        OnWay,
        Arrived,

        Frozen,
        RequestСancellation,
        Canceled,
    }
}
