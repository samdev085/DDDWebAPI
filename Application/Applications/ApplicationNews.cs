using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.ServicesInterface;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ApplicationNews : IApplicationNews
    {

        INews _INews;
        IServiceNews _IServiceNews;

        public ApplicationNews(INews INews, IServiceNews IServiceNews)
        {
            _INews = INews;
            _IServiceNews = IServiceNews;
        }

        public async Task AddNews(News news)
        {
            await _IServiceNews.AddNews(news);
        }

        public async Task UpdateNews(News news)
        {
            await _IServiceNews.UpdateNews(news);
        }

        public async Task<List<News>> ListNewsActive()
        {
            return await _IServiceNews.ListNewsActive();
        }

        public async Task Add(News Object)
        {
            await _INews.Add(Object);
        }

        public async Task Delete(News Object)
        {
            await _INews.Delete(Object);
        }

        public async Task<List<News>> GetAll()
        {
            return await _INews.GetAll();
        }

        public async Task<News> GetById(int Id)
        {
            return await _INews.GetById(Id);
        }

        public async Task Update(News Object)
        {
            await _INews.Update(Object);
        }

        
    }
}
