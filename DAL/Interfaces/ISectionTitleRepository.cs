using DAL.Interfaces;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISectionTitleRepository : IRepository<SectionTitle>
    {
        public Task<SectionTitle> FindByNameAsync(string SectionTitleName);
    }
}
