using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;
using TelegramBdBot;

namespace TelegramBotv1._1
{
    public static class Quartz1
    {
        public static async Task Repeat(long id, bool allert)
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();



            IJobDetail job = JobBuilder.Create<JobForBot>()
                .WithIdentity("job1", "group1")
                .UsingJobData("chatId", $"{id}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(DateTimeOffset.Now.AddSeconds(5))
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            if (allert)
            {
                await scheduler.Start();
                await scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                await scheduler.Shutdown();
            }

            await Task.Delay(TimeSpan.FromSeconds(1000));
            await scheduler.Shutdown();
        }
    }
}
