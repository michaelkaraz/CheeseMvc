using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMvc.Data;
using CheeseMvc.Models;
using CheeseMvc.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMvc.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext _context;

        public MenuController(CheeseDbContext context)
        {
            this._context = context;
        }

        // GET: Menu
        public ActionResult Index()
        {
            List<Menu> menus = _context.Menus.ToList();
            return View(menus);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                _context.Menus.Add(newMenu);
                _context.SaveChanges();
                return Redirect("/Menu");
            }

            return View(addMenuViewModel);
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = _context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            Menu menu = _context.Menus.SingleOrDefault(m => m.ID == id);
            
            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = _context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = _context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(  menu, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseId = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = _context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseId)
                    .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        Cheese = _context.Cheeses.SingleOrDefault(c => c.ID == cheeseId),
                        Menu = _context.Menus.SingleOrDefault(m => m.ID == menuID)
                    };
                    _context.CheeseMenus.Add(menuItem);
                    _context.SaveChanges();
                }

                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }

            return View(addMenuItemViewModel);
        }
    }

   
}