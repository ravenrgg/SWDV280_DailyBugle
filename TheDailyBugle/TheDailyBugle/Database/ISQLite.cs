using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;

namespace TheDailyBugle
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
