using Microsoft.AspNetCore.Mvc;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers{
    public class BaiBaoController : Controller{
        private readonly WebsiteTuthienContext context;
        public BaiBaoController(WebsiteTuthienContext _context){
            context = _context;
        }
    }
}