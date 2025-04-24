using System.Collections.Generic;

namespace BetterER.Models
{
    public class BasicEntity
    {
        public string Name { get; set; }
        public List<BasicAttribute> Attributes { get; set; }

        public BasicEntity()
        {
            Attributes = new List<BasicAttribute>();
        }
    }
}
