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
        public double Price { get; set; }
        public int ConsecutiveIncreases { get; set; }
        public double PercentageIncrease { get; set; }
        public double OverallChange { get; set; }
        public int CoinId { get; set; }
        public virtual Coins Coin { get; set; }
    }
}
