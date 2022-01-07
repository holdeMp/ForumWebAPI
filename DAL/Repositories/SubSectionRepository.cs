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
    public class SubSectionRepository:ISubSectionRepository
    {
        private readonly ForumDbContext _db;
        public SubSectionRepository(ForumDbContext forumDbContext)
        {
            _db = forumDbContext;
        }
        public async Task AddAsync(SubSection entity)
        {
            await _db.SubSections.AddAsync(entity);
        }

        public IQueryable<SubSection> FindAll()
        {
            return _db.SubSections.Select(i => i);
        }

        public Task<SubSection> GetByIdAsync(int id)
        {
            return Task.Run(() => { return _db.SubSections.Find(id); });
        }

        public void Update(SubSection entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
        public Task<SubSection> FindByNameAsync(string sectionName)
        {
            return Task.Run(() => { return _db.SubSections.FirstOrDefault(i => i.Name == sectionName); }); ;
        }
    }
}
