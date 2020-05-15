using System.Collections.Generic;

namespace BetterER.Models
{
    public class BasicDiagram
    {
        public List<BasicEntity> BasicEntities { get; set; }
        public BasicDiagram()
        {
            BasicEntities = new List<BasicEntity>();
        }
    }
}
