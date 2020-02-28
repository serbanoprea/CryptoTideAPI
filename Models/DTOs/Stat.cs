using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class Stat
    {
        public double Current { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
        public IEnumerable<double> Values { get; set; }
    }
}
