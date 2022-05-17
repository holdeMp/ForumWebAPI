using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class ThemeModel
    {
        public int Id { get; set; }
        public int ViewCount { get; set; }
        public int SubSectionId { get; set; }
        public virtual ICollection<AnswerModel> Answers { get; set; }
        public string Name { get; set; }
    }
}
