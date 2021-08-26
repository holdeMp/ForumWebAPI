﻿using DAL.Interfaces;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
    }
}