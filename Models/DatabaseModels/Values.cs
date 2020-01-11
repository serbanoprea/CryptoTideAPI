using System;

namespace Models.DatabaseModels
{
    public partial class Values
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public decimal Price { get; set; }
        public long Volume { get; set; }
        public long Marketcap { get; set; }
        public decimal Performance1m { get; set; }
        public decimal Performance1w { get; set; }
        public decimal Performance1y { get; set; }
        public decimal Performance24h { get; set; }
        public decimal Performance6m { get; set; }
        public decimal PerformanceYtd { get; set; }
        public decimal LiquidityAsk { get; set; }
        public decimal LiquidityBid { get; set; }
        public decimal LiquidityRatio { get; set; }
        public int Hour { get; set; }
        public DateTime ValueDate { get; set; }
        public int CoinId { get; set; }

        public virtual Coins Coin { get; set; }
    }
}
