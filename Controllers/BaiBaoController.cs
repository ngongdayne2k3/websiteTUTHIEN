using Microsoft.AspNetCore.Mvc;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers{
    public class BaiBaoController : Controller{
        WebsiteTuthienContext db = new WebsiteTuthienContext();
        public IActionResult Index(){
            return View();
        }
    }
}