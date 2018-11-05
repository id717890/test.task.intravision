namespace drinks.dal.mapping
{
    using System.Data.Entity.ModelConfiguration;
    using domain.@interface.entities;

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id");
            Property(x => x.Name).HasColumnName("name").IsRequired();
            Property(x => x.SecretKey).HasColumnName("secret").IsRequired();
        }
    }
}
