﻿namespace drinks.dal
{
    using System.Data.Entity;
    using mapping;
    using domain.@interface.entities;

    public class DrinkContext: DbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<User> Users { get; set; }
        public DrinkContext() : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new DrinkMap());
            modelBuilder.Configurations.Add(new CoinMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
