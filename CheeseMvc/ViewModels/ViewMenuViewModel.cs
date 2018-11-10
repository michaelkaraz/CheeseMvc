using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMvc.Models;

namespace CheeseMvc.ViewModels
{
    public class ViewMenuViewModel
    {
        public List<CheeseMenu> Items { get; set; }
        public Menu Menu { get; set; }
    }
}
