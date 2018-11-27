﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TheDailyBugle.Models
{
    public class Comic
    {
        public int ComicId { get; set; }
        public int ComicTitleId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
