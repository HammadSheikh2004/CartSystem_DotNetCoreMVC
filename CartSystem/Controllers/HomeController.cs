using CartSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CartSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _db;
        private readonly IWebHostEnvironment _web;

        public HomeController(MyDbContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(ProductModels models, IFormFile file)
        {
            models.Id = Guid.NewGuid();
            var path = Path.Combine(_web.WebRootPath, "ProductImages");
            var fileName = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(fileName);
            var ds = DateTime.Now.Millisecond;
            string imageName = "Products" + ds + extension;
            var fileSavePath = Path.Combine(path, imageName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            models.Image = imageName;

            _db.Products.Add(models);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult FetchData()
        {
            var data = _db.Products.ToList();
            return View(data);
        }



    }
}