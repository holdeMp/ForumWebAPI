using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISectionService:ICrud<SectionModel>
    {
        public Task<SectionModel> FindByName(string SectionTitleName);
    }
}
