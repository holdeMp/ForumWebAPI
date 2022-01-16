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
            entity.Sections = entity.Sections.ToList();
            db.Entry(entity).State = EntityState.Modified;
            db.Entry(entity).Collection(i=>i.Sections).IsModified = true;
            db.SaveChanges();
        }
        public Task<SectionTitle> GetByIdAsync(int id)
        {
            return Task.Run(() => { return db.SectionTitles.Find(id); });
        }

        public Task<SectionTitle> FindByNameAsync(string SectionTitleName)
        {
            return Task.Run(() => { return db.SectionTitles.FirstOrDefault(i=>i.Name== SectionTitleName); }); ;
        }

        public void Delete(SectionTitle entity)
        {
            db.SectionTitles.Remove(entity);
        }
    }
}
