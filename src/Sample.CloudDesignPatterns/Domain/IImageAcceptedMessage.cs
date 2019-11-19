using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.CloudDesignPatterns.Domain
{
    public interface IImageAcceptedMessage
    {
        string Name { get; set; }
    }
}
