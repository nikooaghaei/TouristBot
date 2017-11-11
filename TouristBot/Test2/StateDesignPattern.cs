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
            if (person.State == "Start")    ////////////inke dar state e city bkhahad ostan ra avaz knad dar enteha ezafe shavad.
            {
                string message = "به راهنمای ایران گردی خوش آمدید :)";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                Console.WriteLine("avali");
                person.State = "Province";

            }
            else if (person.State == "Province" && person.Text == "انتخاب استان")
            {
                string message = "استان مورد نظر خود را انتخاب کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.ProvinceState() };

                bot.MakeRequestAsync(reg);
                person.State = "City";
                Console.WriteLine("dovomi");
            }
            else if (person.State == "City" && (person.Text == "تهران" || person.Text == "فارس"))// agar yek ostane alaki type shavad, nabaid peiqame entekhabe shahr namayesh dade shavad , ama in baiad dynamic shavad
            {
                string message = "شهر مورد نظر خود را انتخاب کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.CityState(person.Text) };

                bot.MakeRequestAsync(reg);
                person.State = "Options";
                Console.WriteLine("dovomi");
            }
            else if (person.State == "Options" && person.Text == "")//////////chetori hame shahraye hame ostanaro check knm?? age ye halate koliam baram baz moshkele chert type kardan hast
            {
                string message = "استان مورد نظر خود را انتخاب کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.ProvinceState() };

                bot.MakeRequestAsync(reg);
                person.State = "";
                Console.WriteLine("");
            }

            else if (person.State == "Options" && person.Text == "خروج")////////////in halat baarye tamame state ha baiad gozashte shavad
            {
                string message = "خدانگهدار. می توانید مجدد آغاز کنید :)";
                var reg = new SendMessage(person.ChatID, message);
                bot.MakeRequestAsync(reg);
                person.State = "Start";
                Console.WriteLine("sevomi");
            }

            // user.State = person.State;          /////comment**
            //_context.SaveChanges();
        }
    }





}