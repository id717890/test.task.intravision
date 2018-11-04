using drinks.domain.@interface.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks.infrastructure.Response
{
    public class DrinkListResponse: BaseResponse
    {
        public IEnumerable<Drink> Drinks { get; set; }
    }
}
