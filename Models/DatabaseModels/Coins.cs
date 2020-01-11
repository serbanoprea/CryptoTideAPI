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
        public string Identifier { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Values> Values { get; set; }
    }
}
