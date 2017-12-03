using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristBot.Linq;

namespace TouristBot.DBHelper
{
    static class DBH
    {
        static Tourist_BotEntities temp = new Tourist_BotEntities();
        public static void AddPlace(string pr, string c, string pl, string d)
        {
            temp.AddPlace(pr, c, pl, d);
        }
    }
}
