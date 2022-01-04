using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Section
    {
        
        public int SectionID { get; set; }
        public string Name { get; set; }
        public int SectionTitleId { get; set; }       
        public virtual SectionTitle SectionTitle { get; set; }
        public virtual ICollection<SubSection> SubSections { get; set; }
    }
}
