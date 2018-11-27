using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TheDailyBugle.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public int ComicTitleId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
