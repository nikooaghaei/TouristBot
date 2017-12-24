using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi.Requests;

namespace TouristBot.Model
{
    public class Person
    {
        public string State;
        public long ChatID;
        public string Text;
        public NetTelegramBotApi.Types.PhotoSize[] Pic = new NetTelegramBotApi.Types.PhotoSize[1];
        public string np_pro = "def";
        public string np_city = "def";
        public string np_name = "def";
        public string np_desc = "def";
        public Boolean np_IsP = false;


    }
}
