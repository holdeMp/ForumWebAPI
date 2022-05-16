using Data.Interfaces;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ISectionRepository SectionRepository { get;}
        ISectionTitleRepository SectionTitleRepository { get; }
        ISubSectionRepository SubSectionRepository { get; }
        IThemeRepository ThemeRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> SaveAsync();
    }

}
