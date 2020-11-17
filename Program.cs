using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotv1._1;

namespace TelegramBdBot
{
    static class Program
    {
        public static void Main()
        {
            string token = File.ReadAllText(@"E:\develop\TelegramBot\BotToken.txt");
            TelegramBotClient bot = new TelegramBotClient(token);
            Console.WriteLine($"@{bot.GetMeAsync().Result.Username} start...");

            bot.OnMessage += async (s, arg) =>
            {
                string msgText = arg.Message.Text;
                long chatId = arg.Message.Chat.Id;

                if (msgText == "/start")
                {
                    await bot.SendTextMessageAsync(chatId, "Добрый день! Бот запущен.\n/getallert - включение системы оповещения\n/stop - выключение системы оповещения");
                }

                if (msgText == "/getallert")
                {
                    await bot.SendTextMessageAsync(chatId, "Система оповещения включена! Ежедневно в 7.00 вам будут приходить уведомления о ДР ваших друзей!");
                    await Quartz1.Repeat(chatId, true);
                }

                if (arg.Message.Text == "/stop")
                {
                    await bot.SendTextMessageAsync(chatId, "Система оповещения отключена");
                    await Quartz1.Repeat(chatId, false);
                }
            };
            bot.StartReceiving();
            Console.ReadLine();

            
        }

    }
}
