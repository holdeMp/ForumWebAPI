using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class SubSection
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
        public ICollection<Theme> Themes { get; set; }
    }
}
