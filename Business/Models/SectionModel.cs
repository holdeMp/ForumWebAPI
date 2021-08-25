using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Models
{
    public class SectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> SubSectionsIds { get; set; }

    }
}
