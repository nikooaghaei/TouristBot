using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTelegramBotApi.Types;

namespace TouristBot.Keyboard
{
    public class BotKeyboard
    {
        public ReplyKeyboardMarkup GetOptions()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("مشاهده موارد"),
                    new KeyboardButton("خروج"),
                    new KeyboardButton("اضافه کردن مکان جدید")
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
        public ReplyKeyboardMarkup GetPlaces()
        {
            ReplyKeyboardMarkup key = new ReplyKeyboardMarkup();
            key.Keyboard = new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("انصراف"),
                }
            };
            key.ResizeKeyboard = true;
            return key;
        }
    }
}