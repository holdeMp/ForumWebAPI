using System.Collections.Generic;

namespace Business.Models
{
    public class SectionModel
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public int SectionTitleId { get; set; }
        public SectionTitleModel SectionTitle { get; set; }
        public ICollection<int> SubSectionsIds { get; set; }

    }
}
