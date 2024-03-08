using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()            //getting categories
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);           //return categories to view/index to display them
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString()) 
            //{
            //    ModelState.AddModelError("Name", "The displayOrder cannot exactly match the Name");
            //}
            if(ModelState.IsValid) 
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index"/*,"Category"*/);   //Redirecting to index (action name). we skip controller name as we are in same controller
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category categoryFromDb1 = _db.Categories.Find(id);   // find works only on prim.key
            if(categoryFromDb==null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid) 
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index"/*,"Category"*/);   //Redirecting to index (action name). we skip controller name as we are in same controller
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category categoryFromDb1 = _db.Categories.Find(id);   // find works only on prim.key
            if(categoryFromDb==null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index"/*,"Category"*/);   //Redirecting to index (action name). we skip controller name as we are in same controller
        }
    }
}
