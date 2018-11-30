﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDailyBugle.Models;

namespace TheDailyBugle.Services
{
    public interface IComicParserService
    {
        List<ComicTitle> GetComicTitles();
        List<Comic> GetComics(string url, int count, bool recursiveSearch);
    }
}