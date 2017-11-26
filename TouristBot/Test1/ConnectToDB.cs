using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using TouristBot.Linq;
using TouristBot.Model;
using TouristBot.Test2;

namespace TouristBot.Test1
{
    public class ConnectToDB
    {
        private static string Token = "453855694:AAGFzQIu3Ss4CkemsuebcqlYItT7GwDqCM4";

        public static void Run_Bot()
        {
            /*first
               //Create bot
               var bot = new TelegramBot(Token);
               var me = bot.MakeRequestAsync(new GetMe()).Result;


               //creating for each user a specific id to have it's own state
               var userId = new Dictionary<long, int>();
               int cnt = 0;
               
               long offset = 0;
               long iid = 0;

               Tourist_BotEntities _context = new Tourist_BotEntities();

               while (true)
               {
                   var updates = bot.MakeRequestAsync(new GetUpdates() { Offset = offset }).Result;
                   try
                   {
                       foreach (var update in updates)
                       {
                           Console.WriteLine(update.Message.Text);
                           string text = update.Message.Text;
                           long chatId = update.Message.Chat.Id;
                           if (text == "/start")
                           {
                               string message = "welcom :)";
                               var reg = new SendMessage(chatId, message);
                               bot.MakeRequestAsync(reg);
                           }
                           else
                           {
                               City city = new City();
                               city.Id = iid;
                               city.Message = text;
                               _context.Cities.Add(city);
                               try { _context.SaveChanges(); }
                               catch (Exception e) { Console.WriteLine(e.Message); }
                               string message = "undefined request !";
                               var reg = new SendMessage(chatId, message);
                               bot.MakeRequestAsync(reg);
                           }
                           offset = update.UpdateId + 1;
                           iid++;
                       }
                   }
                   catch (Exception e) { Console.WriteLine(e.Message); }
               }*/

            /*second*/
            //making the bot
            var bot = new TelegramBot(Token);
            var me = bot.MakeRequestAsync(new GetMe()).Result;


            //creating for each user a specific id to have it's own state
            var userId = new Dictionary<long, int>();
            int cnt = 0;

            //getting the list of person in ram
            List<Person> persons = new List<Person>();
            long offset = 0;
            long iid = 0;
            //long examIDs = 0;
            //long QuestionIDs = 0;

            Tourist_BotEntities _context = new Tourist_BotEntities();

            while (true)
            {
                var updates = bot.MakeRequestAsync(new GetUpdates() { Offset = offset }).Result;
                try
                {
                    foreach (var update in updates)
                    {
                        long chatId = update.Message.Chat.Id;

                        if (!userId.ContainsKey(chatId))
                        {
                            userId.Add(chatId, cnt);
                            Person p = new Person();
                            p.ChatID = chatId;
                            p.State = "Start";
                            persons.Add(p);
                            cnt++;
                        }
                        persons[userId[chatId]].Text = update.Message.Text;
                       // Console.WriteLine(persons[userId[chatId]].State);
                        StateDesignPattern userState = new StateDesignPattern();
                     //   Console.WriteLine("mire");
                        userState.CheckState(persons[userId[chatId]], bot,  _context );
                        offset = update.UpdateId + 1;
                      //  Console.WriteLine("mire2");
                        iid++;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
