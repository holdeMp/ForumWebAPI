using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Theme
    {
        public int Id { get; set; }
        public int SubSectionId { get; set; }
        public int ViewCount { get; set; }
        public Answer MainAnswer { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int AnswersCount { get; set; }

    }
}
