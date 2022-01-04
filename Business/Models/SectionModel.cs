using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
