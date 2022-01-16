using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public int AuthorId { get; set; }
        public int? ReferenceAnswerId { get; set; }
        public string Content { get; set; }
    }
}
