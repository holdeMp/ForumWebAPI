
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Theme
    {
        public int Id { get; set; }
        public int ViewCount { get; set; }
        public int SubSectionId { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public string Name { get; set; }
    }
}
