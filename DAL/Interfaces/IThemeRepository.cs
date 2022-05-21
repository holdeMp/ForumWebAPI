using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IThemeRepository : IRepository<Theme>
    {
        Task<Theme> FindByNameAsync(string themeName);
    }
}
