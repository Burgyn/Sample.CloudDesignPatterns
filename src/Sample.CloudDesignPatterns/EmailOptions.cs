using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.CloudDesignPatterns
{
    public class EmailOptions
    {
        public string ApiKey { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }
    }
}
