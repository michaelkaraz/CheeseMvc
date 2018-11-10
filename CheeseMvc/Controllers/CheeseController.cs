using System.Collections.Generic;
using System.Linq;
using CheeseMvc.Data;
using CheeseMvc.Models;
using CheeseMvc.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CheeseMvc.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext _context;
        public CheeseController( CheeseDbContext context)
        {
            _context = context;
           
        }

        public IActionResult Index()
        {

          //  List<Cheeses> CheeseID = _context.Cheeses.ToList();  // CheeseData.GetAll();
            IList<Cheese> cheeses = _context.Cheeses.Include(c => c.Category).ToList();
            // ViewBag.CheeseID = Cheeses;
            return View(cheeses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(_context.Categories.ToList());
            return View(addCheeseViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory =
                    _context.Categories.SingleOrDefault(c => c.ID == addCheeseViewModel.CategoryID);
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };
               // CheeseData.Add(newCheese);
                _context.Cheeses.Add(newCheese);
                _context.SaveChanges();
                return Redirect("/Cheeses");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            return View(_context.Cheeses.ToList());
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (var cheeseId in cheeseIds)
            {
               // CheeseData.Remove(cheeseId);
                Cheese cheesetoRemove = _context.Cheeses.SingleOrDefault(c => c.ID == cheeseId);
                if(cheesetoRemove !=null)
                 _context.Cheeses.Remove(cheesetoRemove);
            }

            _context.SaveChanges();
            return Redirect("/Cheeses");
        }

        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                return Redirect("/Category");
                }

            CheeseCategory theCategory = _context.Categories
                .Include(cat => cat.Cheeses)
                .Single(cat => cat.ID == id);

            //Link syntax query from the other side of the view
            //IList<Cheeses> theCheeses = _context.Cheeses
            //    .Include(c => c.Category)
            //    .Where(c => c.CategoryID == id)
            //    .ToList();

            ViewBag.title = "Cheeses in category " + theCategory.Name;

            return View("Index", theCategory.Cheeses);
        }

    }
}