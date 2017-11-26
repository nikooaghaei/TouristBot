using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi.Types;
using TouristBot.Linq;
using TouristBot.Model;

namespace TouristBot.Keyboard
{
    public class BotKeyboard
    {

        Tourist_BotEntities _context = new Tourist_BotEntities();
        public ReplyKeyboardMarkup StartState()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("انتخاب استان"),
                    new KeyboardButton("خروج"),
                    new KeyboardButton("اضافه کردن مکان جدید")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
        public ReplyKeyboardMarkup ProvinceState()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            var places = _context.Places.ToList();  ////har ostan chanbar chap mishavad-be tedade shahrhayash ke be aan tedad dar jadvale places amade. ba eslah jadval ha eslah shaavd.
            //Console.WriteLine(places.Count);
            key.Keyboard = new KeyboardButton[places.Count()][];
            for (int j = 0; j < places.Count(); j++)
            {
                key.Keyboard[j] = new KeyboardButton[1];
                key.Keyboard[j][0] = new KeyboardButton(places[j].Province);
            }
            //for (int i = 0; i < temp.Count; i++)
                //{

                //    key.Keyboard = new KeyboardButton[][]
                //    {

                //         new KeyboardButton[]
                //        {

                //            new KeyboardButton(temp[i].Province)
                //        }
                //    };
                //}
                key.ResizeKeyboard = true;
            return key;
        }
        public ReplyKeyboardMarkup CityState(string province)
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();

            var places = _context.Places.Where(x => x.Province == province).ToList();
            key.Keyboard = new KeyboardButton[places.Count()][];
            for (int j = 0; j < places.Count(); j++)
            {
                key.Keyboard[j] = new KeyboardButton[1];
                key.Keyboard[j][0] = new KeyboardButton(places[j].City);
            }
           
            //if (province == "تهران")
            //{
            //    key.Keyboard = new KeyboardButton[][]
            //    {
            //        new KeyboardButton[]
            //        {
            //            new KeyboardButton("شهرری"),
            //            new KeyboardButton("اسلامشهر"),
            //            new KeyboardButton("ورامین"),
            //            new KeyboardButton("خروج")
            //        }
            //    };
            //}
            //else if (province == "فارس")
            //{
            //    key.Keyboard = new KeyboardButton[][]
            //     {
            //        new KeyboardButton[]
            //        {
            //            new KeyboardButton("داراب"),
            //            new KeyboardButton("شیراز"),
            //            new KeyboardButton("جهرم"),
            //            new KeyboardButton("خروج")
            //        }
            //     };
            //}
            key.ResizeKeyboard = true;
            return key;
        }
        public ReplyKeyboardMarkup AddPlace()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("انصراف")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
    }
}