using Business.Models;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IThemeService : ICrud<ThemeModel>
    {
        public Task<ThemeModel> FindByName(string themeName);
    }
}
