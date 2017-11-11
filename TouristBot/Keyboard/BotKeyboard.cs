using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi.Types;
using TouristBot.Model;

namespace TouristBot.Keyboard
{
    public class BotKeyboard
    {

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
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("تهران"),
                    new KeyboardButton("فارس")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
        public ReplyKeyboardMarkup CityState(string province)
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            if (province == "تهران")
            {
                key.Keyboard = new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("شهرری"),
                        new KeyboardButton("اسلامشهر"),
                        new KeyboardButton("ورامین"),
                        new KeyboardButton("خروج")
                    }
                };
            }
            else if (province == "فارس")
            {
                key.Keyboard = new KeyboardButton[][]
                 {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("داراب"),
                        new KeyboardButton("شیراز"),
                        new KeyboardButton("جهرم"),
                        new KeyboardButton("خروج")
                    }
                 };
            }
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