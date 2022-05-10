namespace EcommerceProject.Domain.AggregatesRoot.OrderAggregate
{
    public enum OrderStatus
    {
        Placed = 1,
        Canceled = 2,
        Processing = 3,
        BeingTransported = 4,
        Sent = 5,
        WaitingForPayment = 6
    }
}
