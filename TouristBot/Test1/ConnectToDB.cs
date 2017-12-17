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
using System.IO;

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
                            //p.pic = update.Message.Photo.ToString();
                            persons.Add(p);
                            cnt++;


                        }
                        var check = _context.Users.Where(x => x.Id == chatId);
                        //Console.WriteLine(check.ToList()[0].Id);
                        if (check.Count() != 0)
                        {
                            var temp = _context.Users.FirstOrDefault(x => x.Id == chatId);
                            _context.Users.Remove(temp);
                        }
                            // _context.Users.SqlQuery("DELETE FROM[Tourist_Bot].[dbo].[User] WHERE[Tourist_Bot].[dbo].[User].Id = " + chatId);

                        User u = new User();

                        u.Id = chatId;
                        if (!persons[userId[chatId]].np_IsP)
                            u.Message = update.Message.Text;
                        else
                            u.Message = "IsPhoto";
                        u.State = persons.Where(x => x.ChatID == chatId).ToList()[0].State;
                        u.Username = update.Message.Chat.Username;
                        u.Firstname = update.Message.Chat.FirstName;
                        u.Lastname = update.Message.Chat.LastName;
                        _context.Users.Add(u);
 


                        //    Console.WriteLine("avali");
                              
                            
                        //else
                        
                          //  Console.Write("DELETE FROM[Tourist_Bot].[dbo].[User] WHERE[Tourist_Bot].[dbo].[User].Id = " + chatId);
                          //  _context.Users.SqlQuery("DELETE FROM[Tourist_Bot].[dbo].[User] WHERE[Tourist_Bot].[dbo].[User].Id = 60163330");
                        //_context.Users.S

                        //_context.Users.
                             
                            try { _context.SaveChanges(); }
                        catch (Exception e) { Console.WriteLine(e.Message + "DB in ConnectToDB"); }




                        //MemoryStream ms = new MemoryStream();
                        if (!persons[userId[chatId]].np_IsP)
                            persons[userId[chatId]].Text = update.Message.Text;
                        else
                        {
                            persons[userId[chatId]].Pic = update.Message.Photo;

                            //persons[userId[chatId]].Pic.FileId = "fuck";
                              Console.Write("ax" + persons[userId[chatId]].Pic[0].FileId);
                            // string FUCKU = persons[userId[chatId]].Pic.FileId;

                        }
                            // Console.WriteLine(persons[userId[chatId]].State);
                        StateDesignPattern userState = new StateDesignPattern();
                     //   Console.WriteLine("mire");
                        userState.CheckState(persons[userId[chatId]], bot,  _context );
                        offset = update.UpdateId + 1;
                      //  Console.WriteLine("mire2");
                        iid++;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message+ e.Source + e.GetType() + e.Data +"Base error"); }
            }
        }
    }
}
