using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    class PopulationAggregationGraphDTO
    {
        public IEnumerable<DateTime> Dates { get; set; }
        public IEnumerable<float> Changes { get; set; }
    }
}
