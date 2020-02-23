using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTOs
{
    public class OverviewDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public int IncreaseHours { get; set; }
        public double Change { get; set; }
        public double IncreasePerc { get; set; }
        public int RankChange { get; set; }
        public int Rank { get; set; }
    }
}
