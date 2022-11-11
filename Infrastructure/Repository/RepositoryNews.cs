using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryNews : RepositoryGenerics<News>, INews
    {

        private readonly DbContextOptions<Context> _optionsbuilder;

        public RepositoryNews()
        {
            _optionsbuilder = new DbContextOptions<Context>();
        }

        public async Task<List<News>> ListNews(Expression<Func<News, bool>> exNews)
        {
            using (var db = new Context(_optionsbuilder))
            {
                return await db.News.Where(exNews).AsNoTracking().ToListAsync();
            }
        }
    }
}
