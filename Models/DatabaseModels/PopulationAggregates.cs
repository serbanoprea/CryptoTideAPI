using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DatabaseModels
{
    public class PopulationAggregate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double AverageChange { get; set; }
        public double StDevChange { get; set; }
        public double OverallScore { get; set; }
        public bool HigherThan25Perc { get; set; }
        public bool HigherThanMedian { get; set; }
        public bool HigherThan75Perc { get; set; }
        public bool PopulationMinimum { get; set; }
    }
}
