using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class AddThemeModel
    {
        public int SubSectionId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public AnswerModel Answer { get; set; }
    }
}
