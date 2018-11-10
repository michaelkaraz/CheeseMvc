using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CheeseMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMvc.ViewModels
{
    public class AddCheeseViewModel
    {
        

        [Required]
        [Display(Name = "Cheeses Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        public int CategoryID { get; set; }

        public List<SelectListItem> CheeseCategories { get; set; }
        public string SaveChangesError { get; set; }

        public AddCheeseViewModel(List<CheeseCategory> categories)
        {
            CheeseCategories = new List<SelectListItem>();
            foreach (var c in categories)
            {
                CheeseCategories.Add(new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            });
            }
        }

        public AddCheeseViewModel()
        {
            
        }

    }
}
