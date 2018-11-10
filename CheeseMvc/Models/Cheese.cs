using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMvc.Models
{
    public class Cheese
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryID  { get; set; }//not ideal
        public CheeseCategory Category { get; set; }//not ideal


        public List<CheeseMenu> CheeseMenus { get; set; }

        //private static int _nextId = 1;

        //public Cheeses()
        //{
        //  CheeseId =  _nextId++;

        //}
    }
}
