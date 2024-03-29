﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Data;
using Entities.Migrations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    class AnswerRepository: BaseRepository<Answer>, IAnswerRepository
    {
        private EnglishContext _context;
        public AnswerRepository(EnglishContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Answer>> GetAnswersByExercise(Guid id, bool trackChanges)
        {
            return await FindByCondition( p => p.Id.Equals(id), trackChanges).ToListAsync();
        }


    }
}

