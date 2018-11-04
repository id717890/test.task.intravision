using System;
using drinks.domain.@interface.entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace drinks.web.Models
{
    public class DrinkViewModel
    {
        public class DrinkList
        {
            public IEnumerable<Drink> Drinks { get; set; }
        }

        public class NewDrink
        {
            [Required]
            [Display(Name = "Наименование")]
            public string Caption { get; set; }

            [Required]
            [Display(Name = "Цена")]
            public int Cost { get; set; }

            [Required]
            [Display(Name = "Кол-во")]
            public int Count { get; set; }

            [Display(Name = "Изображение")]
            public string Image { get; set; }

            public HttpPostedFileBase ImageFile { get; set; }
        }

        public class ParticularDrinkModel 
        {
            public long Id { get; set; }

            [Required]
            [Display(Name = "Наименование")]
            public string Caption { get; set; }

            [Required]
            [Display(Name = "Цена")]
            public int Cost { get; set; }

            [Required]
            [Display(Name = "Кол-во")]
            public int Count { get; set; }

            [Display(Name = "Изображение")]
            public string Image { get; set; }

            public HttpPostedFileBase ImageFile { get; set; }

        }
    }
}