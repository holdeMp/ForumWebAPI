using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class SectionTitle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Section> Sections { get; set; }
    }
}
