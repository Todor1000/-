using Microsoft.AspNetCore.Mvc;
using Локален_Бюлетински_Помошник.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Локален_Бюлетински_Помошник.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        // GET: Profile/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Profile/Upload
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

                // Optionally: Save the file path in the database or associate with the logged-in user
                TempData["Message"] = "Image uploaded successfully!";
                return RedirectToAction("Upload");
            }

            TempData["Message"] = "No image uploaded or file is empty.";
            return View();
           
        }
    }
}
