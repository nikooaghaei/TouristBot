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
using TouristBot.DBHelper;

namespace TouristBot.Test2
{
    public class StateDesignPattern
    {
        BotKeyboard keyboard = new BotKeyboard();
        //DBH DB = new DBH();

       // public DBHelper Db { get => db; set => db = value; }

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
            if (person.State == "Start")
            ////////////inke dar state e city bkhahad ostan ra avaz knad dar enteha ezafe shavad.
            {
                string message = "به راهنمای ایران گردی خوش آمدید :)";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                //   Console.WriteLine("avali");
                person.State = "Options";//taeen ink mkhahad ostan ezafe knd ya list bebinad

            }
            else if (person.State == "Options" && person.Text == "انتخاب استان")
            {
                string message = "استان مورد نظر خود را انتخاب کنید:";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.ProvinceState() };

                bot.MakeRequestAsync(reg);
                person.State = "City";
                //    Console.WriteLine("dovomi");
            }
            //else if (person.State == "City" && (person.Text == "تهران" || person.Text == "فارس"))// agar yek ostane alaki type shavad, nabaid peiqame entekhabe shahr namayesh dade shavad , ama in baiad dynamic shavad
            else if (person.State == "City" && person.Text != "انصراف")
            {
                string message = "شهر مورد نظر خود را انتخاب کنید:";
                long proID = _context.Provinces.Where(x => x.Name == person.Text).ToList()[0].Id;
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.CityState(proID) };

                bot.MakeRequestAsync(reg);
                person.State = "Places";
                //  Console.WriteLine("dovomi");
            }
            /* else if (person.State == "Places" && person.Text == "")
                 //////////chetori hame shahraye hame ostanaro check knm?? age ye halate koliam baram baz moshkele chert type kardan hast
             {
                 try
                 {
                     string message = "استان مورد نظر خود را انتخاب کنید:";
                     var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.ProvinceState() };

                     bot.MakeRequestAsync(reg);
                     person.State = "";
                     Console.WriteLine("");
                 }
                 catch (Exception e)
                 {

                     Console.WriteLine(e.Message);
                 }

             }*/   //hich idei nadaram ina chian ... comment mikonam ziresh ok mikonam

            else if (person.State == "Places" && person.Text != "انصراف")
            {

                string message = "مکان های دیدنی به شرح زیر است. برای اطلاعات بیشتر مکان مورد نظر را انتخاب کنید";
                long CitID = _context.Cities.Where(x => x.Name == person.Text).ToList()[0].Id;
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.PlaceState(CitID) };

                bot.MakeRequestAsync(reg);
                person.State = "Desc";
            }
            else if (person.State == "Desc" && person.Text != "انصراف")
            {

                string message = _context.Places.Where(x => x.Name == person.Text).ToList()[0].Description;
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                person.State = "Options";
            }
            //else if (person.State == "Places" && person.Text == "خروج")////////////in halat baarye tamame state ha baiad gozashte shavad
            else if (person.Text == "خروج")
            {
                string message = "خدانگهدار. می توانید مجدد آغاز کنید :)";
                var reg = new SendMessage(person.ChatID, message);
                bot.MakeRequestAsync(reg);
                person.State = "Start";
                //  Console.WriteLine("sevomi");
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
                NewPlace.np_pro = person.Text;
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
                Console.WriteLine(NewPlace.np_pro + NewPlace.np_city + NewPlace.np_name + NewPlace.np_desc);

                /*  Place pl = new Place();
                  City c = new City();
                  Province pr = new Province();
                  pr.Name = NewPlace.np_pro;
                  c.Name = NewPlace.np_city;
                  pl.Description = NewPlace.np_desc;
                  pl.Name = NewPlace.np_name;
                  _context.Provinces.Add(pr);
                  try { _context.SaveChanges(); }
                  catch (Exception e) { Console.WriteLine(e.Message); }
                  //while(_context.Provinces.Where(x => x.Name == NewPlace.np_pro).ToList()[0].Id == 0) { }
                  c.Province_Id = _context.Provinces.Where(x => x.Name == NewPlace.np_pro ).ToList()[0].Id;
                  _context.Cities.Add(c);
                  try { _context.SaveChanges(); }
                  catch (Exception e) { Console.WriteLine(e.Message); }
                  //while (_context.Cities.Where(x => x.Name == NewPlace.np_city).ToList()[0].Id == 0) { }
                  pl.City_Id = _context.Cities.Where(x => x.Name == NewPlace.np_city).ToList()[0].Id;
                  _context.Places.Add(pl);
                  */

                DBH.AddPlace(NewPlace.np_pro, NewPlace.np_city, NewPlace.np_name, NewPlace.np_desc);

                try { _context.SaveChanges(); }
                catch (Exception e) { Console.WriteLine(e.Message); }


                string message = "با تشکر از شما مکان جدید ثبت شد";
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                person.State = "Options";
            }
            else if (person.Text == "انصراف")
            {
                string message = "";
                if (person.State == "addPlace1" || person.State == "addPlace2" || person.State == "addPlace3" || person.State == "addPlace4")
                {
                    message = "اضافه کردن مکان جدید لغو شد";
                }
                else
                {
                    message = "جست و جو مکان لغو شد";
                }
                var reg = new SendMessage(person.ChatID, message) { ReplyMarkup = keyboard.StartState() };

                bot.MakeRequestAsync(reg);
                person.State = "Options";
            }
            // user.State = person.State;          /////comment**
            //_context.SaveChanges();
        }
    }





}