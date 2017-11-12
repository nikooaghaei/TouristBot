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
        public void CheckState(Person person, TelegramBot bot, Tourist_BotEntities _context )
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
                person.State = "Options";

            }
            else if (person.State == "Options" && person.Text == "انتخاب استان")
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
            else if (person.State == "Options" && person.Text == "اضافه کردن مکان جدید")
            {
                string message = "نام استان را وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.AddPlace() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace1";
            }
            else if (person.State == "addPlace1" && person.Text != "انصراف")
                
            {
                NewPlace.np_state = person.Text;
                string message = "نام شهر را وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.AddPlace() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace2";
            }
            else if (person.State == "addPlace2" && person.Text != "انصراف")

            {
                NewPlace.np_city = person.Text;
                string message = "نام مکان را وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.AddPlace() };

                bot.MakeRequestAsync(reg);
                person.State = "addPlace3";
            }
            else if (person.State == "addPlace3" && person.Text != "انصراف")
            {
                NewPlace.np_name = person.Text;
                string message = "توضیحاتی در مورد این مکان وارد کنید";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.AddPlace() };
                bot.MakeRequestAsync(reg);
                person.State = "addPlace4";
            }

            else if (person.State == "addPlace4" && person.Text != "انصراف")
            {
                //if ok
                NewPlace.np_desc = person.Text;
                Console.WriteLine(NewPlace.np_state + NewPlace.np_city+ NewPlace.np_name+ NewPlace.np_desc);

                Place p = new Place();
                p.City = NewPlace.np_city;
                p.Description = NewPlace.np_desc;
                p.Name = NewPlace.np_name;
                p.Province = NewPlace.np_state;
                _context.Places.Add(p);
                try { _context.SaveChanges(); }
                catch (Exception e) { Console.WriteLine(e.Message); }


                string message = "با تشکر از شما مکان جدید ثبت شد";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };
                
                bot.MakeRequestAsync(reg);
                person.State = "Options";
            }
            else if (person.Text == "انصراف")
            {
                string message = "اضافه کردن مکان جدید لغو شد";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                person.State = "Options";
            }
            // user.State = person.State;          /////comment**
            //_context.SaveChanges();
        }
    }





}