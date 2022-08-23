using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebeziumTest.Models
{
    public class JsonModel<T>
    {
        public PayloadModel<T> Payload { get; set; }
    }

    public class PayloadModel<T>
    {
        public T Before { get; set; }
        public T After { get; set; }
        public string Op { get; set; }
    }
}
