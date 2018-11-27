using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TheDailyBugle.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
