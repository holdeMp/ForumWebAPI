using DAL.Interfaces;
using Data.Entities;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISectionTitleRepository : IRepository<SectionTitle>
    {
        public Task<SectionTitle> FindByNameAsync(string SectionTitleName);
    }
}
