﻿using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface INewsService
    {
        IEnumerable<News> GetNewsByDate(DateTime from, DateTime to);
        IEnumerable<string> GetTopTenWordsInNews();
        IEnumerable<News> SearchByText(string text);

    }
}