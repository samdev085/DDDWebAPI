﻿using Application.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationNews : IGenericApplication<News>
    {
        Task AddNews(News news);
        Task UpdateNews(News news);
        Task<List<News>> ListNewsActive();
    }
}
