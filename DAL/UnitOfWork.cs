using DAL.Interfaces;
using DAL.Repositories;
using Data;
using ForumAPI.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ForumDbContext db;
        private SectionRepository sectionsRepository;
        public UnitOfWork(ForumDbContext forumDbContext)
        {
            db = forumDbContext;
        }
        public SectionRepository Sections
        {
            get
            {
                if (sectionsRepository == null)
                    sectionsRepository = new SectionRepository(db);
                return sectionsRepository;
            }
        }
        public ISectionRepository SectionRepository { get => sectionsRepository; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveAsync()
        {
            return db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
