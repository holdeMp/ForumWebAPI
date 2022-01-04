using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISectionTitleService : ICrud<SectionTitleModel>
    {
        public Task<SectionTitleModel> FindByName(string SectionTitleName);
    }
}
