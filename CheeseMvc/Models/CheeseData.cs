using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMvc.Models
{
    public class CheeseData
    {
        private static readonly List<Cheese> cheeses = new List<Cheese>();

        //GetAll
        public static List<Cheese> GetAll()
        {
            return cheeses;
        }
        //Add
        public static void Add(Cheese cheese)
        {
            cheeses.Add(cheese);
        }
        //Rem
        public static void Remove(int id)
        {
            cheeses.Remove(GetById(id));
        }
        //GetById

        public static Cheese GetById(int id)
        {
            return cheeses.SingleOrDefault(x => x.ID == id);
        }
    }
}
