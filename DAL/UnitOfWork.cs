using DAL.Interfaces;
using DAL.Repositories;
using Data;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ForumDbContext _db;
        private SectionRepository _sectionsRepository;
        private SectionTitleRepository _sectionsTitleRepository;
        private SubSectionRepository _subSectionRepository;
        private ThemeRepository _themeRepository;
        private AnswerRepository _answerRepository;

        public UnitOfWork(ForumDbContext forumDbContext)
        {
            _db = forumDbContext;
        }
        public SectionTitleRepository SectionTitle => _sectionsTitleRepository ??= new SectionTitleRepository(_db);
        public ThemeRepository Themes => _themeRepository ??= new ThemeRepository(_db);

        public AnswerRepository Answers
        {
            get { return _answerRepository ??= new AnswerRepository(_db); }
        }
        public SectionRepository Sections
        {
            get { return _sectionsRepository ??= new SectionRepository(_db); }
        }
        public SubSectionRepository SubSections
        {
            get { return _subSectionRepository ??= new SubSectionRepository(_db); }
        }
        public ISectionRepository SectionRepository => Sections;
        public ISubSectionRepository SubSectionRepository => SubSections;

        public ISectionTitleRepository SectionTitleRepository => SectionTitle;
        public IThemeRepository ThemeRepository => Themes;

        public IAnswerRepository AnswerRepository => Answers;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
