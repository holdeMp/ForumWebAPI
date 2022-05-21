using System.Collections.Generic;

namespace Data.Entities
{
    public class SectionTitle
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
