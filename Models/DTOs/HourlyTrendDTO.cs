using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class HourlyTrendDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public double Price { get; set; }
        public int ConsecutiveIncreases { get; set; }
        public double PercentageIncrease { get; set; }
        public double OverallChange { get; set; }
    }
}
