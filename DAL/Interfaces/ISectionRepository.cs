using Data.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        public Task<Section> FindByNameAsync(string sectionName);
    }
}
