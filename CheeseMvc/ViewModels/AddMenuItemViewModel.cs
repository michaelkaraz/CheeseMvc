using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CheeseMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CheeseMvc.ViewModels
{
    public class AddMenuItemViewModel
    {
        [Required]
        public int MenuID { get; set; }
        [Required]
        public int CheeseID { get; set; }
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; } 

        public AddMenuItemViewModel()
        {
           
        }
        public AddMenuItemViewModel(Menu menu, List<Cheese> cheeses)
        {
            this.Menu = menu;
            Cheeses = new List<SelectListItem>();
            foreach (var c in cheeses)
            {
                Cheeses.Add(new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.ID.ToString()
                });
            }
           // Menu
        }
    }
}
