using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ForumDbContext:DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = (@"Data Source=DESKTOP-FM6GLLE\MSSQLSERVER02;Initial Catalog=ForumDB;Integrated Security=SSPI");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<SectionTitle> SectionTitles { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<Theme> Themes { get; set; }
    }
}
