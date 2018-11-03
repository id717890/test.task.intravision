using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using drinks.domain.@interface.entities;

namespace drinks.dal.mapping
{
    public class DrinkMap: EntityTypeConfiguration<Drink>
    {
        public DrinkMap()
        {
            ToTable("Drinks");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id");
            Property(x => x.Caption).HasColumnName("caption");
            Property(x => x.Image).HasColumnName("image");
            Property(x => x.Cost).HasColumnName("cost");
        }
    }
}
