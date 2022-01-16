using DAL.Interfaces;
using DAL.Repositories;
using Data;
using Data.Interfaces;
using Data.Repositories;
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
        private SectionTitleRepository sectionsTitleRepository;
        private SubSectionRepository _subSectionRepository;
        private ThemeRepository _themeRepository;
        public UnitOfWork(ForumDbContext forumDbContext)
        {
            db = forumDbContext;
        }
        public SectionTitleRepository SectionTitle
        {
            get
            {
                if (sectionsTitleRepository == null)
                    sectionsTitleRepository = new SectionTitleRepository(db);
                return sectionsTitleRepository;
            }
        }
        public ThemeRepository Themes
        {
            get
            {
                if (_themeRepository == null)
                    _themeRepository = new ThemeRepository(db);
                return _themeRepository;
            }
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
        public SubSectionRepository SubSections
        {
            get
            {
                if (_subSectionRepository == null)
                    _subSectionRepository = new SubSectionRepository(db);
                return _subSectionRepository;
            }
        }
        public ISectionRepository SectionRepository => Sections;
        public ISubSectionRepository SubSectionRepository => SubSections;

        public ISectionTitleRepository SectionTitleRepository => SectionTitle;
        public IThemeRepository ThemeRepository => Themes;

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
