using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class CoinAggregateDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double? DayChange { get; set; }
        public double? WeekChange { get; set; }
        public double? MonthChange { get; set; }
        public bool Min24hVolatility { get; set; }
        public bool Small24hVolatility { get; set; }
        public bool Medium24hVolatility { get; set; }
        public bool High24hVolatility { get; set; }
        public bool Max24hVolatility { get; set; }
        public bool Min24hChange { get; set; }
        public bool Small24hChange { get; set; }
        public bool Medium24hChange { get; set; }
        public bool High24hChange { get; set; }
        public bool Max24hChange { get; set; }
        public bool MinWeekVolatility { get; set; }
        public bool SmallWeekVolatility { get; set; }
        public bool MediumWeekVolatility { get; set; }
        public bool HighWeekVolatility { get; set; }
        public bool MaxWeekVolatility { get; set; }
        public bool MinWeekChange { get; set; }
        public bool SmallWeekChange { get; set; }
        public bool MediumWeekChange { get; set; }
        public bool HighWeekChange { get; set; }
        public bool MaxWeekChange { get; set; }
        public bool MinMonthChange { get; set; }
        public bool SmallMonthChange { get; set; }
        public bool MediumMonthChange { get; set; }
        public bool HighMonthChange { get; set; }
        public bool MaxMonthChange { get; set; }
        public bool MinMonthVolatility { get; set; }
        public bool SmallMonthVolatility { get; set; }
        public bool MediumMonthVolatility { get; set; }
        public bool HighMonthVolatility { get; set; }
        public bool MaxMonthVolatility { get; set; }
    }
}
