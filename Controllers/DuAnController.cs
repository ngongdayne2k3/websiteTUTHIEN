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

        public IActionResult CreateDuAn(){
            ViewData["MaDanhMucDa"] = new SelectList(context.TableDanhMucDuAns,"MaDanhMucDa","TenDanhMucDa");
            ViewData["MaTinhThanh"] = new SelectList(context.TableTinhThanhs,"MaTinhThanh","TenTinhThanh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDuAn(TableDuAn duAn){
            if(ModelState.IsValid){
                var userId = HttpContext.Session.GetInt32("UserId");
                duAn.MaNguoiDung = userId ?? 0;
                duAn.DaDuyetBai = false;
                duAn.DaKetThucDuAn = false;
                duAn.Ngaybatdau = DateTime.Today;
                duAn.SoTienHienTai = 0;
                context.Add(duAn);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexDuAnUser));
            }
            ViewData["MaDanhMucDa"] = new SelectList(context.TableDanhMucDuAns,"MaDanhMucDa","TenDanhMucDa");
            ViewData["MaTinhThanh"] = new SelectList(context.TableTinhThanhs,"MaTinhThanh","TenTinhThanh");
            return View(duAn);
        }
    }
}
