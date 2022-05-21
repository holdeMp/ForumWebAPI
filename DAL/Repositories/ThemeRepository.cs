using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
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

        public async Task<Theme> FindByNameAsync(string themeName)
        {
            var theme  = await _db.Themes.FirstOrDefaultAsync(i => i.Name==themeName);
            return theme;
        }

        public IQueryable<Theme> FindAll()
        {
            return _db.Themes.Select(i => i); 
        }

        public Task<Theme> GetByIdAsync(int id)
        {
            return Task.Run(() => _db.Themes.Find(id));
        }

        public void Update(Theme theme)
        {
            var entry = _db.Themes.First(e => e.Id == theme.Id);
            _db.Entry(entry).CurrentValues.SetValues(theme);
            _db.SaveChanges();
        }
    }
}
