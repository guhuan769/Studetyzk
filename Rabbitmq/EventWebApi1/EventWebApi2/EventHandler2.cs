using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    [EventName("OrderDeleted")]
    public class EventHandler2 : JsonIntegrationEventHandler<OrderData>
    {
        public override Task HandleJson(string eventName, OrderData? eventData)
        {
            if (eventName.Equals("OrderCreated"))
            {
                Console.WriteLine("收到了订单。eventData=" + eventData);
            }
            return Task.CompletedTask;
        }
    }

    public record OrderData(long Id, string Name, DateTime Date);
}
