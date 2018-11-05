namespace drinks.dal.mapping
{
    using System.Data.Entity.ModelConfiguration;
    using domain.@interface.entities;

    public class DrinkMap: EntityTypeConfiguration<Drink>
    {
        public DrinkMap()
        {
            ToTable("Drinks");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id");
            Property(x => x.Caption).HasColumnName("caption").IsRequired();
            Property(x => x.Image).HasColumnName("image").IsRequired();
            Property(x => x.Cost).HasColumnName("cost").IsRequired();
            Property(x => x.Count).HasColumnName("count").IsRequired();
        }
    }
}
