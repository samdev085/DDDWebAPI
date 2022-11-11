using Domain.Interfaces;
using Domain.Interfaces.ServicesInterface;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NewsService : IServiceNews
    {
        private readonly INews _INews;

        public NewsService(INews INews)
        {
            _INews = INews;
        }

        public async Task AddNews(News news)
        {
            var validateTitle = news.ValidatePropertyString(news.Title, "Title");
            var validateInformation = news.ValidatePropertyString(news.Information, "Information");

            if (validateTitle && validateInformation)
            {
                news.DateModification = DateTime.Now;
                news.DateRegister = DateTime.Now;
                news.Active = true;
                await _INews.Add(news);
            }
        }
        public async Task<List<News>> ListNewsActive()
        {
            return await _INews.ListNews(n => n.Active);
        }
        public async Task UpdateNews(News news)
        {
            var validateTitle = news.ValidatePropertyString(news.Title, "Title");
            var validateInformation = news.ValidatePropertyString(news.Information, "Information");

            if (validateTitle && validateInformation)
            {
                news.DateModification = DateTime.Now;
                news.DateRegister = DateTime.Now;
                news.Active = true;
                await _INews.Update(news);
            }
        }
    }
}
