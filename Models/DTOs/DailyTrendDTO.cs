using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class DailyTrendDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double MinPrice { get; set; }
        public double Price25Perc { get; set; }
        public double PriceMedian { get; set; }
        public double Price75Perc { get; set; }
        public double MaxPrice { get; set; }
        public double OverallChange { get; set; }
        public double SmallPercChange { get; set; }
        public double MediumPercChange { get; set; }
        public double HighPercChange { get; set; }
        public double MedianChange { get; set; }
        public double Median75PercChange { get; set; }
        public double MedianMaxChange { get; set; }
        public int ConsecutiveSmallChange { get; set; }
        public int ConsecutiveMediumChange { get; set; }
        public int ConsecutiveHighChange { get; set; }
    }
}
