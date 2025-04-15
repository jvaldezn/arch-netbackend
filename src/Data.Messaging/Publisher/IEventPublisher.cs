using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Messaging.Publisher
{
    public interface IEventPublisher
    {
        Task Publish<T>(T message) where T : class;
    }
}
