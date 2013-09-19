using System;
using Glimpse.Core.Message;

namespace Glimpse.Castle.Windsor
{
    public class ComponentModelMessage : IMessage
    {
        public ComponentModelMessage()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public TimeSpan FromFirst { get; set; }
        public TimeSpan FromLast { get; set; }
        public string EventName { get; set; }
        public string Service { get; set; }
        public string Implementation { get; set; }
        public string Lifestyle { get; set; }
        public string Interceptors { get; set; }
    }
}