using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class DuAnController : Controller
    {
        private readonly WebsiteTuthienContext context;
        public DuAnController(WebsiteTuthienContext _context){
            context = _context;
        }

        public async Task<IActionResult> IndexDuAn(){
            return View(await context.TableDuAns.ToListAsync());
        }

        public async Task<IActionResult> IndexDuAnUser(){
            var userId = HttpContext.Session.GetInt32("UserId");
            var products = await context.TableDuAns
            .Where(p => p.MaNguoiDung == userId)
            .ToListAsync();
            return View(products);
        }

        public IActionResult ChiTietDuAn(int id){
            var duan = context.TableDuAns.FirstOrDefault(p=> p.MaDanhMucDa == id);
            if (duan == null){
                return NotFound();
            }
            return View(duan);
        }

        // public IActionResult CreateDuAn(){
        //     ViewBag.danhMucDuAn = new SelectList(context.TableDanhMucDuAns,"MaDanhMucDa","TenDanhMucDa");
        //     return View();
        // }
    }
}
