using System.Collections.Generic;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

namespace Glimpse.Castle.Windsor
{
    public class ComponentMessagesConverter : SerializationConverter<IEnumerable<ComponentModelMessage>>
    {
        public override object Convert(IEnumerable<ComponentModelMessage> messages)
        {
            var root = new TabSection("Event", "Service", "Implementation", "Lifestyle", "Interceptors", "From Request Start", "From Last");

            foreach (var message in messages)
            {
                root.AddRow().
                     Column(message.EventName).
                     Column(message.Service).
                     Column(message.Implementation).
                     Column(message.Lifestyle).
                     Column(message.Interceptors).
                     Column(message.FromFirst).
                     Column(message.FromLast).
                     Style(FormattingKeywords.Info);
            }

            return root.Build();
        }
        
    }
}