using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class CoinAggregateDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Symbol { get; set; }
        public double AverageDayChange { get; set; }
        public double SumDayChange { get; set; }
        public double StDevDayChange { get; set; }
        public int DayRecords { get; set; }
        public double AverageWeekChange { get; set; }
        public double SumWeekChange { get; set; }
        public int WeekRecords { get; set; }
        public double StDevWeekChange { get; set; }
        public double WeekVolatility { get; set; }
        public double DayVolatility { get; set; }
        public bool HigherThanMedianDay { get; set; }
        public bool HigherThan25PercDay { get; set; }
        public bool HigherThan75PercDay { get; set; }
        public bool HigherThanAverageDay { get; set; }
        public bool HigherThanMedianWeek { get; set; }
        public bool HigherThan25PercWeek { get; set; }
        public bool HigherThan75PercWeek { get; set; }
        public bool HigherThanAverageWeek { get; set; }
        public bool HigherVolatilityThan75PercDay { get; set; }
        public bool HigherVolatilityThan25PercDay { get; set; }
        public bool HigherVolatilityThanMedianDay { get; set; }
        public bool HigherThanAverageVolatilityDay { get; set; }
        public bool HigherVolatilityMedianWeek { get; set; }
        public bool HigherVolatilityThan75PercWeek { get; set; }
        public bool HigherVolatilityThan25PercWeek { get; set; }
        public bool HigherThanAverageVolatilityWeek { get; set; }
        public bool MinWeekVolatility { get; set; }
        public bool MaxWeekVolatility { get; set; }
        public bool MinDayVolatility { get; set; }
        public bool MaxDayVolatility { get; set; }
    }
}
