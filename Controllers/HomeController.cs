using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Локален_Бюлетински_Помошник.Models;

namespace Локален_Бюлетински_Помошник.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Request()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        // GET: Home/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Home/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(ImageUploadViewModel model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                // Create the uploads directory if it doesn't exist
                if (!Directory.Exists(_uploadFolder))
                {
                    Directory.CreateDirectory(_uploadFolder);
                }

                // Generate a unique filename to avoid name conflicts
                var fileName = Path.GetFileName(model.Image.FileName);
                var filePath = Path.Combine(_uploadFolder, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                // Optionally, save file path in the database, associate with user, etc.
                TempData["Message"] = "Image uploaded successfully!";
                return RedirectToAction("Upload");
            }

            TempData["Message"] = "No image uploaded or file is empty.";
            return View();
        }
    }
}
