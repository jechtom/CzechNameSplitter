using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CzechNameSplitter
{
    public class ParsedName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public double Score { get; set; }

        public string OriginalName { get; set; }
    }
}
