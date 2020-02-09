using System;

namespace Models.DatabaseModels
{
    public partial class Values
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public double Price { get; set; }
        public long Volume { get; set; }
        public long Marketcap { get; set; }
        public double LiquidityAsk { get; set; }
        public double LiquidityBid { get; set; }
        public double LiquidityRatio { get; set; }
        public double Performance1m { get; set; }
        public double Performance1w { get; set; }
        public double Performance1y { get; set; }
        public double Performance24h { get; set; }
        public double Performance6m { get; set; }
        public double PerformanceYtd { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int CoinId { get; set; }

        public virtual Coins Coin { get; set; }
    }
}
