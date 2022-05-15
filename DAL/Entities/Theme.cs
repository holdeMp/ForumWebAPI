using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Theme
    {
        public int Id { get; set; }
        public int ViewCount { get; set; }
        public int SubSectionId { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public string Name { get; set; }
    }
}
