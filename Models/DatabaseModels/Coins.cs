using System.Collections.Generic;

namespace Models.DatabaseModels
{
    public partial class Coins
    {
        public Coins()
        {
            Values = new HashSet<Values>();
        }

        public int Id { get; set; }
        public int PreviousRank { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Values> Values { get; set; }
    }
}
