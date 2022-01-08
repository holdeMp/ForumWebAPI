using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public int AuthorId { get; set; }
        public int? ReferenceAnswerId { get; set; }
        public string Content { get; set; }
    }
}
