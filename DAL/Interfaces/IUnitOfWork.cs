using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ISectionRepository SectionRepository { get;}
        ISectionTitleRepository SectionTitleRepository { get; }
        Task<int> SaveAsync();
    }

}
