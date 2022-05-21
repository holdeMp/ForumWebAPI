using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ISectionTitleRepository : IRepository<SectionTitle>
    {
        public Task<SectionTitle> FindByNameAsync(string SectionTitleName);
    }
}
