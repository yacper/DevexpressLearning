using PrismFullApp.Services.Interfaces;

namespace PrismFullApp.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
