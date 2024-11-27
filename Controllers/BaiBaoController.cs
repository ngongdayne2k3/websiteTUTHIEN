using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class BaiBaoController : Controller
    {
        private readonly WebsiteTuthienContext context;
        private readonly IWebHostEnvironment env;
        public BaiBaoController(WebsiteTuthienContext _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }

        public IActionResult Index()
        {
            var baiBao = context.TableBaiBaos.ToList();
            return View(baiBao);
        }


        public IActionResult CreateBaiBao()
        {
            ViewData["DanhMucBaiBao"] = new SelectList(context.TableDanhMucBaiBaos, "MaDanhMucBaiBao", "TenDanhMucBaiBao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBaiBao(TableBaiBao baiBao, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    baiBao.HinhanhBaiBao = "/images/" + fileName;
                }
                baiBao.NgayDangBaiBao = DateTime.Now;
                context.TableBaiBaos.Add(baiBao);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucBaiBao"] = new SelectList(context.TableDanhMucBaiBaos, "MaDanhMucBaiBao", "TenDanhMucBaiBao");
            return View(baiBao);
        }
    }
}