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
    public class ThemeRepository : IThemeRepository
    {
        private readonly ForumDbContext _db;
        public ThemeRepository(ForumDbContext forumDbContext)
        {
            _db = forumDbContext;
        }
        public async Task AddAsync(Theme entity)
        {
            await _db.Themes.AddAsync(entity);
        }

        public void Delete(Theme entity)
        {
            _db.Themes.Remove(entity);
        }

        public IQueryable<Theme> FindAll()
        {
            return _db.Themes.Select(i => i); 
        }

        public Task<Theme> GetByIdAsync(int id)
        {
            return Task.Run(() => { return _db.Themes.Find(id); });
        }

        public void Update(Theme theme)
        {
            var entry = _db.Themes.First(e => e.Id == theme.Id);
            _db.Entry(entry).CurrentValues.SetValues(theme);
            _db.SaveChanges();
        }
    }
}
