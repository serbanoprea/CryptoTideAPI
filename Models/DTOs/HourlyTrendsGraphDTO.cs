using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class HourlyTrendsGraphDTO
    {
        public string Name { get; set; }
        public int MaxConsecutiveIncreases { get; set; }
        public double MaxIncreaseSeries { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
        public IEnumerable<double> Changes { get; set; }
        public IEnumerable<double> Prices { get; set; }
        public IEnumerable<double> ConsecutiveIncreasePerc { get; set; }
    }
}
