using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testTask
{
    internal class Trade
    {
        public string lastUpdateId { get; set; }
        public string[][] bids { get; set; }
        public string[][] asks { get; set; }
    }
}
