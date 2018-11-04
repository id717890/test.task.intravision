namespace drinks.dal.Migrations
{
    using domain.@interface.entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<drinks.dal.DrinkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(drinks.dal.DrinkContext context)
        {
            context.Database.ExecuteSqlCommand("delete from Drinks");

            context.Drinks.AddOrUpdate(
                new Drink { Id = 1, Caption= "—ок апельсиновый", Cost = 10, IsDeleted = false, Count = 50, Image = @"/Images/orange.jpg"},
                new Drink { Id = 1, Caption= "—ок €блочный", Cost = 10, IsDeleted = false, Count = 50, Image = @"/Images/apple.jpg" },
                new Drink { Id = 1, Caption= "—ок мультифрутовый", Cost = 10, IsDeleted = false, Count = 50, Image = @"/Images/multy.jpg" },
                new Drink { Id = 1, Caption= "—ок томатный", Cost = 10, IsDeleted = false, Count = 50, Image = @"/Images/tomato.jpg" },
                new Drink { Id = 1, Caption= "—ок абрикосовый", Cost = 10, IsDeleted = false, Count = 50, Image = @"/Images/apricote.jpg" }
            );
            context.SaveChanges();

            context.Coins.AddOrUpdate(
                new Coin { Id = 1, Caption = "1 рубль", Count = 1, IsAllowed = true, IsDeleted = false, Value = 1 },
                new Coin { Id = 2, Caption = "2 рубл€", Count = 3, IsAllowed = true, IsDeleted = false, Value = 2 },
                new Coin { Id = 5, Caption = "5 рублей", Count = 3, IsAllowed = true, IsDeleted = false, Value = 5 },
                new Coin { Id = 10, Caption = "10 рублей", Count = 5, IsAllowed = true, IsDeleted = false, Value = 10}
            );
            context.SaveChanges();

            context.Users.AddOrUpdate(
                new User { Id = 1, IsDeleted = false, Name = "Admin", SecretKey = "secret12345"}
            );
            context.SaveChanges();


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
