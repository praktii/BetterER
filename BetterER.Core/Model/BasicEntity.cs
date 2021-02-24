using System.Collections.Generic;

namespace BetterER.Core.Model
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
