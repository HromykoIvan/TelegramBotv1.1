using Quartz;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotv1._1.Context;

namespace TelegramBdBot
{
    public class JobForBot : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Start Execute task..");
            JobDataMap dateMap = context.JobDetail.JobDataMap;

            string replyMsg = string.Empty;
            string token = File.ReadAllText(@"E:\develop\TelegramBot\BotToken.txt");
            long chatIdForTask = dateMap.GetLong("chatId");

            TelegramBotClient bot = new TelegramBotClient(token);
            await bot.SendTextMessageAsync(chatIdForTask, "Начинается поиск..");

            using (VkBotContext db = new VkBotContext())
            {
                Console.WriteLine("Start searching into db..");
                string data = $"{DateTime.UtcNow.Day}/{DateTime.UtcNow.Month}";

                var friend = db.Friends.Where(f => f.DateBirthday.StartsWith(data));

                foreach (var item in friend)
                {
                    replyMsg += $"{item.Id} - {item.FirstName} {item.LastName} - Data of Birth - {item.DateBirthday}, location - {item.City}\n";
                }
            }

            if (!string.IsNullOrEmpty(replyMsg))
            {
                await bot.SendTextMessageAsync(chatIdForTask, replyMsg);
            }
            else
            {
                await bot.SendTextMessageAsync(chatIdForTask, "Данные не найдены.");
            }

        }

    }
}
