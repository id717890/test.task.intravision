namespace drinks.dal.mapping
{
    using System.Data.Entity.ModelConfiguration;
    using domain.@interface.entities;

    public class CoinMap : EntityTypeConfiguration<Coin>
    {
        public CoinMap()
        {
            ToTable("Coins");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id");
            Property(x => x.Caption).HasColumnName("caption").IsRequired();
            Property(x => x.Count).HasColumnName("count").IsRequired();
            Property(x => x.Value).HasColumnName("value").IsRequired();
            Property(x => x.IsAllowed).HasColumnName("is_allowed").IsRequired();
        }
    }
}
