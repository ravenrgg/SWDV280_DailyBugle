using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TheDailyBugle.Models
{
    public abstract class Destination
    {
        public abstract int Insert(IDbConnection connection);
    }
}
