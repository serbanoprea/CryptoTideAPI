using System.Collections.Generic;

namespace Models.DatabaseModels
{
    public partial class Coins
    {
        public Coins()
        {
            CoinAggregates = new HashSet<CoinAggregate>();
            DailyTrends = new HashSet<DailyTrend>();
            HourlyTrends = new HashSet<HourlyTrend>();
            Values = new HashSet<Values>();
        }

        public int Id { get; set; }
        public int PreviousRank { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CoinAggregate> CoinAggregates { get; set; }
        public virtual ICollection<DailyTrend> DailyTrends { get; set; }
        public virtual ICollection<HourlyTrend> HourlyTrends { get; set; }
        public virtual ICollection<Values> Values { get; set; }
    }
}
