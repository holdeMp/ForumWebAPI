using DAL.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SectionTitleRepository : ISectionTitleRepository
    {
        private readonly ForumDbContext db;
        public SectionTitleRepository(ForumDbContext forumDbContext)
        {
            db = forumDbContext;
        }
        public async Task AddAsync(SectionTitle entity)
        {
            await db.SectionTitles.AddAsync(entity);
        }

        public IQueryable<SectionTitle> FindAll()
        {
            return db.SectionTitles.Select(i => i); 
        }

        public void Update(SectionTitle entity)
        {
            db.Entry(entity).State = EntityState.Modified; 
        }
        public Task<SectionTitle> GetByIdAsync(int id)
        {
            return Task.Run(() => { return db.SectionTitles.Find(id); });
        }
    }
}
