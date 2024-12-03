using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using websiteTUTHIEN.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace websiteTUTHIEN.Controllers
{
    public class BaiBaoController : Controller
    {
        private readonly WebsiteTuthienContext context;

        public BaiBaoController(WebsiteTuthienContext _context)
        {
            context = _context;
        }


        //Phân trang, tìm kiếm, sắp xếp, chưa phân loại?
        public ViewResult IndexBaiBao(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSoreParm = sortOrder == "Date" ? "date_desc" : "Date";
            var baiBao = from s in context.TableBaiBaos select s;
            switch (sortOrder)
            {
                case "name_desc":
                    baiBao = baiBao.OrderByDescending(s => s.TenBaiBao);
                    break;
                case "Date":
                    baiBao = baiBao.OrderBy(s => s.NgayDangBaiBao);
                    break;
                case "date_desc":
                    baiBao = baiBao.OrderByDescending(s => s.NgayDangBaiBao);
                    break;
                default:
                    baiBao = baiBao.OrderBy(s => s.TenBaiBao);
                    break;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                baiBao = baiBao.Where(p => p.TenBaiBao.Contains(searchString));
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(baiBao.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult ChiTietBaiBao(int? id)
        {
            var baiBao = context.TableBaiBaos.FirstOrDefault(p => p.MaBaiBao == id);
            if (baiBao == null)
            {
                NotFound();
            }
            return View(baiBao);
        }

        [HttpPost]
        public IActionResult ThemBinhLuan(TableBinhLuanBaiBao binhLuan)
        {
            if (ModelState.IsValid)
            {
                binhLuan.NgayBinhLuan = DateOnly.FromDateTime(DateTime.Now);
                context.TableBinhLuanBaiBaos.Add(binhLuan);
                context.SaveChanges();
                return RedirectToAction("ChiTietBaiBao", new { id = binhLuan.MaBaiBao });
            }
            return View(binhLuan);
        }

        [HttpPost]
        public IActionResult SuaBinhLuan(TableBinhLuanBaiBao binhLuan)
        {
            if (ModelState.IsValid)
            {
                var existingComment = context.TableBinhLuanBaiBaos.Find(binhLuan.MaBinhLuanBaiBao);
                if (existingComment != null)
                {
                    existingComment.NoidungBinhLuan = binhLuan.NoidungBinhLuan;
                    context.SaveChanges();
                }
                return RedirectToAction("ChiTietBaiBao", new { id = binhLuan.MaBaiBao });
            }
            return View(binhLuan);
        }

        [HttpPost]
        public IActionResult XoaBinhLuan(int MaBinhLuanBaiBao, int MaBaiBao)
        {
            var comment = context.TableBinhLuanBaiBaos.Find(MaBinhLuanBaiBao);
            if (comment != null)
            {
                context.TableBinhLuanBaiBaos.Remove(comment);
                context.SaveChanges();
            }
            return RedirectToAction("ChiTietBaiBao", new { id = MaBaiBao });
        }
    }
}