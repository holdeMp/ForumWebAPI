using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class SectionTitleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Section> Sections { get; set; }
    }
}
