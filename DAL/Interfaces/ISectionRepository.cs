using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        public Task<Section> FindByNameAsync(string sectionName);
    }
}
