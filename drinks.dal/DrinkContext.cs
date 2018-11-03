using System.Data.Entity;
using drinks.dal.mapping;
using drinks.domain.@interface.entities;

namespace drinks.dal
{
    public class DrinkContext: DbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        public DbSet<Drink> Files { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DrinkContext() : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new DrinkMap());
            modelBuilder.Configurations.Add(new CoinMap());
        }
    }
}
