﻿using DAL.Interfaces;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ForumDbContext db;
        public SectionRepository(ForumDbContext forumDbContext)
        {
            db = forumDbContext;
        }
        public async Task AddAsync(Section entity)
        {
            await db.Sections.AddAsync(entity);
        }

        public IQueryable<Section> FindAll()
        {
            return db.Sections.Select(i => i);
        }

        public void Update(Section entity)
        {
            db.Entry(entity).State = EntityState.Modified; 
        }
    }
}
