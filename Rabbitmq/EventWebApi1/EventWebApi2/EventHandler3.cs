using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    [EventName("OrderDeleted")]
    public class EventHandler3 : DynamicIntegrationEventHandler
    {
        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            Console.WriteLine("收到了订单。eventData=" + eventData);
            return Task.CompletedTask;
        }
    }
}
