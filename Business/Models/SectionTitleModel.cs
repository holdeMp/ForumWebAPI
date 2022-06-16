using System.Collections.Generic;

namespace Business.Models
{
    public class SectionTitleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SectionModel> Sections { get; set; }
    }
}
