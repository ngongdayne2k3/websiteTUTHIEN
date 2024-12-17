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
            var listDuAn = _context.TableDuAns
                .OrderByDescending(duan=>duan.Ngaybatdau)
                .Take(4)
                .ToList();

            var listBaiBao = _context.TableBaiBaos
                .OrderByDescending(baibao=>baibao.NgayDangBaiBao)
                .Take(4)
                .ToList(); 
            // Truyền danh sách bài báo và dự án vào View
            ViewBag.duAns = listDuAn;
            ViewBag.baiBaos = listBaiBao;
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
    }
}
