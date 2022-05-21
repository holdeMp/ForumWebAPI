
using Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AnswerRepository : Interfaces.IAnswerRepository
    {

        private readonly ForumDbContext _db;

        public AnswerRepository(ForumDbContext forumDbContext)
        {
            _db = forumDbContext;
        }
        public async Task AddAsync(Answer entity)
        {
            await _db.Answers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public void Delete(Answer entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Answer> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Answer> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Answer entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
