using Application.Interfaces;
using Entities.Entities;
using Entities.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IApplicationNews _IApplicationNews;

        public NewsController(IApplicationNews IApplicationNews)
        {
            _IApplicationNews = IApplicationNews;
        }


        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ListNews")]
        public async Task<List<News>> ListNews()
        {
            return await _IApplicationNews.ListNewsActive();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AddNews")]
        public async Task<List<Notify>> AddNews(NewsModel news)
        {
            var newNews = new News();
            newNews.Title = news.Title;
            newNews.Information = news.Information;
            newNews.UserId = await ReturnIdUserLogged();
            await _IApplicationNews.AddNews(newNews);

            return newNews.Notifications;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/UpdateNews")]
        public async Task<List<Notify>> UpdateNews(NewsModel news)
        {
            var newNews = await _IApplicationNews.GetById(news.IdNews);
            newNews.Title = news.Title;
            newNews.Information = news.Information;
            newNews.UserId = await ReturnIdUserLogged();
            await _IApplicationNews.UpdateNews(newNews);

            return newNews.Notifications;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/DeleteNews")]
        public async Task<List<Notify>> DeleteNews(NewsModel news)
        {
            var newNews = await _IApplicationNews.GetById(news.IdNews);

            await _IApplicationNews.Delete(newNews);

            return newNews.Notifications;

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetNewsById")]
        public async Task<News> GetNewsById(NewsModel news)
        {
            var newNews = await _IApplicationNews.GetById(news.IdNews);

            return newNews;

        }

        private async Task<string> ReturnIdUserLogged()
        {
            if (User != null)
            {
                var idUser =  User.FindFirst("userId");
                return idUser.Value;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
