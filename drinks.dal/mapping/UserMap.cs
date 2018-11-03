using System.Data.Entity.ModelConfiguration;
using drinks.domain.@interface.entities;

namespace drinks.dal.mapping
{
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
