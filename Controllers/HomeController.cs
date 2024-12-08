using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebsiteTuthienContext _context;

        public HomeController(ILogger<HomeController> logger, WebsiteTuthienContext context)
        {
            _logger = logger;
            _context = context ;
        }

        public IActionResult Index()
        {
            var baiBaos = _context.TableBaiBaos.ToList();

            // Truyền danh sách bài báo vào View
            return View(baiBaos);
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
    }
}
