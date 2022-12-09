using WPFCustomControl.Services.Interfaces;

namespace WPFCustomControl.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
