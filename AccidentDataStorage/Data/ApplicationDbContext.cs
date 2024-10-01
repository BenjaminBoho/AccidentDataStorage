using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AccidentDataStorage.Models;
using AccidentDataStorage.Models.Accidents;

namespace AccidentDataStorage.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Accidents> Accidents { get; set; } = default!;

        public DbSet<ItemList> ItemList { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemList>()
                .HasKey(il => new { il.ItemGenre, il.ItemValue });

        }
    }
}