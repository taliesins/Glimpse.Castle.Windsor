using System;
using System.Diagnostics;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor.Diagnostics.Helpers;
using Glimpse.Core.Extensibility;

namespace Glimpse.Castle.Windsor
{
    public class WindsorInspector : IInspector
    {
        private IMessageBroker _messageBroker;
        private Func<IExecutionTimer> _timerStrategy;
        private Stopwatch _fromLastWatch;
        public static Func<IKernel> Kernal; 
        
        public void Setup(IInspectorContext context)
        {
            _messageBroker = context.MessageBroker;
            _timerStrategy = context.TimerStrategy;

            var kernal = Kernal();
            kernal.ComponentCreated += ComponentCreated;
            kernal.ComponentDestroyed += ComponentDestroyed;
            kernal.DependencyResolving += DependencyResolving;

        }
        
        private  void DependencyResolving(ComponentModel client, DependencyModel model, object dependency)
        {
            Publish("Resolving", client);
        }

        private  void ComponentCreated(ComponentModel model, object instance)
        {
            Publish("Created", model);
        }

        private  void ComponentDestroyed(ComponentModel model, object instance)
        {
            Publish("Destroyed", model);
        }

        protected  void Publish(string eventName, ComponentModel model)
        {
            if (_timerStrategy != null && _messageBroker != null)
            {
                IExecutionTimer timer = _timerStrategy();
                if (timer != null)
                {
                    var interceptors = "None";

                    if (model.HasInterceptors)
                    {
                        interceptors = String.Join(", ", model.Interceptors.Select(x => x.ToString())); 
                    }
                       
                    _messageBroker.Publish(new ComponentModelMessage()
                                               {
                                                   EventName = eventName,
                                                   Service = String.Join(", ", model.Services.Select(service => service.Name)),
                                                   Implementation = model.Implementation.Name,
                                                   Lifestyle = model.GetLifestyleDescriptionLong(),
                                                   Interceptors = interceptors,
                                                   FromFirst = timer.Point().Offset,
                                                   FromLast = CalculateFromLast(timer)
                                               });
                }
            }
        }

        private  TimeSpan CalculateFromLast(IExecutionTimer timer)
        {
            if (_fromLastWatch == null)
            {
                _fromLastWatch = Stopwatch.StartNew();
                return TimeSpan.FromMilliseconds(0);
            }

            // Timer started before this request, reset it
            if (DateTime.Now - _fromLastWatch.Elapsed < timer.RequestStart)
            {
                _fromLastWatch = Stopwatch.StartNew();
                return TimeSpan.FromMilliseconds(0);
            }

            var result = _fromLastWatch.Elapsed;
            _fromLastWatch = Stopwatch.StartNew();
            return result;
        }

      
    }
}