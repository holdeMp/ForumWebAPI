using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class SubSectionModel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
        public ICollection<int> ThemesIds { get; set; }

    }
}
