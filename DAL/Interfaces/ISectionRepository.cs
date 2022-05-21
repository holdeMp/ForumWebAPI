using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        public Task<Section> FindByNameAsync(string sectionName);
    }
}
