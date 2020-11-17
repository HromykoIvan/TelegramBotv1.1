using Microsoft.EntityFrameworkCore;

namespace TelegramBotv1._1.Context
{
    public partial class VkBotContext : DbContext
    {
        public VkBotContext()
        {
        }

        public VkBotContext(DbContextOptions<VkBotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friend> Friends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VkBot;Trusted_Connection=True;");
            }
        }
    }
}
