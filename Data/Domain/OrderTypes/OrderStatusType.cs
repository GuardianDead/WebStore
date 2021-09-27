namespace WebStore.Domain
{
    public enum OrderStatusType
    {
        AwaitingProcessing = 1,
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
