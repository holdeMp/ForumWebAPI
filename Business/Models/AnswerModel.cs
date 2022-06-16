using Newtonsoft.Json;

namespace Business.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string AuthorId { get; set; }
        public int? ReferenceAnswerId { get; set; }
        public string Content { get; set; }

        [JsonConstructor]
        public AnswerModel(int id, int themeId,string authorId, int? referenceAnswerId,string content)
        {
            Id = id;
            ThemeId = themeId;
            AuthorId = authorId;
            ReferenceAnswerId = referenceAnswerId;
            Content = content;
        }
    }
}
