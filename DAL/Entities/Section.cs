using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubSection> SubSections { get; set; }
    }
}
