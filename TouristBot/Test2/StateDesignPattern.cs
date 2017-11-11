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
            else if (person.State == "options" && person.Text == "اضافه کردن مکان جدید")
            {
                string message = "نام استان را وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetPlaces() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace1";
            }
            else if (person.State == "addPlace1" && person.Text != "انصراف")
            {
                string message = "نام شهر را وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetPlaces() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace2";
            }
            else if (person.State == "addPlace2" && person.Text != "انصراف")
            {
                string message = "توضیحاتی در مورد این مکان وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetPlaces() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace3";
            }
            else if (person.State == "addPlace3" && person.Text != "انصراف")
            {
                string message = "توضیحاتی در مورد این مکان وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetPlaces() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace4";
            }
            else if (person.State == "addPlace4" && person.Text != "انصراف")
            {
                //if ok
                string message = "با تشکر از شما مکان جدید ثبت شد";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetOptions() };

                bot.MakeRequestAsync(reg);
                person.State = "options";
            }
            else if (person.Text == "انصراف")
            {
                string message = "اضافه کردن مکان جدید لغو شد";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.GetOptions() };

                bot.MakeRequestAsync(reg);
                person.State = "options";
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