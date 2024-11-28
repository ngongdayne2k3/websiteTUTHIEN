using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class BaiBaoController : Controller
    {
        private readonly WebsiteTuthienContext context;

        public BaiBaoController(WebsiteTuthienContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var baiBao = context.TableBaiBaos.ToList();
            return View(baiBao);
        }
        
        public IActionResult ChiTietBaiBao(int? id){
            var baiBao = context.TableBaiBaos.FirstOrDefault(p => p.MaBaiBao == id);
            if(baiBao == null) {
                NotFound();
            }
            return View(baiBao);
        }

    }
}