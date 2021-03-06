using System.Collections.Generic;

namespace DAL.Entities
{
    public class SubSection
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
