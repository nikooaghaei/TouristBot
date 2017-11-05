using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using TouristBot.Keyboard;
using TouristBot.Linq;
using TouristBot.Model;

namespace TouristBot.Test2
{
    public class StateDesignPattern
    {
        BotKeyboard keyboard = new BotKeyboard();
        public void CheckState(Person person, TelegramBot bot, Tourist_BotEntities _context)
        {
            //User user = _context.Users.FirstOrDefault(x => x.Id == person.ChatID);  //comment**: if its necessary to keep track of repeated users
            //Console.WriteLine("");
            //if (user == null)
            //{
            //    //Console.WriteLine("");
            //    User newUser = new User();
            //    newUser.Id = person.ChatID;
            //    newUser.Message = person.Text;
            //    newUser.State = person.State;
            //    _context.Users.Add(newUser);
            //    try
            //    {
            //        _context.SaveChanges();
            //    }
            //    catch (Exception e) { Console.WriteLine(e.Message); }
            //}
            if (person.State == "start")
            {
                string message = "به راهنمای ایران گردی خوش آمدید :)";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetOptions() };
                
                bot.MakeRequestAsync(reg);
                Console.WriteLine("avali");
                person.State = "options";
                
            }
            else if (person.State == "options" && person.Text == "مشاهده موارد")
            {
                string message = "هریک از موارد زیر را می توانید انتخاب کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetOptions() };

                bot.MakeRequestAsync(reg);
                person.State = "options";/////unnecessary
                Console.WriteLine("dovomi");
            }
            else if (person.State == "options" && person.Text == "خروج")
            {
                string message = "خدانگهدار. می توانید مجدد آغاز کنید :)";
                var reg = new SendMessage(person.ChatID, message);
                bot.MakeRequestAsync(reg);
                person.State = "start";
                Console.WriteLine("sevomi");
            }

           // user.State = person.State;          /////comment**
            //_context.SaveChanges();
        }
    }





}