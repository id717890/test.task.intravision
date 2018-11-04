using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using drinks.domain.@interface.entities;

namespace drinks.web.Models
{
    public class CoinViewModel
    {
        public class CoinList
        {
            public IEnumerable<Coin> Coins { get; set; }
        }

        public class ParticularCoinModel
        {
            public long Id { get; set; }

            [Display(Name = "Наименование")]
            public string Caption { get; set; }

            [Required]
            [Display(Name = "Кол-во")]
            public int Count { get; set; }

            [Display(Name = "Доступность")]
            public bool IsAllowed { get; set; }
        }
    }
}