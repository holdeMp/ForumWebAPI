using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ForumDbContext db;
        public SectionRepository(ForumDbContext forumDbContext)
        {
            db = forumDbContext;
        }
        public async Task AddAsync(Section entity)
        {
            await db.Sections.AddAsync(entity);
        }

        public IQueryable<Section> FindAll()
        {
            return db.Sections.Select(i => i);
        }

        public Task<Section> GetByIdAsync(int id)
        {
            return Task.Run(() => { return db.Sections.Find(id); });
        }

        public void Update(Section entity)
        {
           db.Entry(entity).State = EntityState.Modified; 
        }
        public Task<Section> FindByNameAsync(string sectionName)
        {
            return Task.Run(() => { return db.Sections.FirstOrDefault(i => i.Name == sectionName); }); ;
        }

        public void Delete(Section entity)
        {
            db.Sections.Remove(entity);
        }
    }
}
