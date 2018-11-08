using System;
using System.Collections.Generic;
using System.Text;
using TheDailyBugle.Models;

namespace TheDailyBugle.Services
{
    interface IComicService
    {
        List<Series> GetSeriesList();

    }
}
