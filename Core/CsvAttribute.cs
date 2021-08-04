using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Core
{
    public class CsvAttribute : Attribute
    {
        public int Order { get; set; }

        public CsvAttribute(int order)
        {
            Order = order;
        }
    }
}
