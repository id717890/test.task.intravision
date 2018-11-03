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
    }
}
