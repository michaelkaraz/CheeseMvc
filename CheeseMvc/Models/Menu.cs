using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace CheeseMvc.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<CheeseMenu> CheeseMenus { get; set; }  = new List<CheeseMenu>();
    }
}