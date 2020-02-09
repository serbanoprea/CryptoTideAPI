using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DatabaseModels
{
    public class HourlyTrend
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public float Price { get; set; }
        public int ConsecutiveIncreases { get; set; }
        public float PercentageIncrease { get; set; }
        public float OverallChange { get; set; }
    }
}
