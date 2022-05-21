
namespace DAL.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public int AuthorId { get; set; }
        public int? ReferenceAnswerId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
