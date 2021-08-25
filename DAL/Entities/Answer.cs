using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int AuthorId { get; set; }
        public int? ReferenceAnswerId { get; set; }
        public string Content { get; set; }
    }
}
