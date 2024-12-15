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

        // Action để hiển thị danh sách bài báo
        public IActionResult BaiBao()
        {
            // Lấy tất cả bài báo từ cơ sở dữ liệu
            var baiBaos = context.TableBaiBaos.ToList();

            // Truyền danh sách bài báo vào View
            return View(baiBaos);
        }

        //Phân trang, tìm kiếm, sắp xếp, chưa phân loại?
        public ViewResult IndexBaiBao(string sortOrder, string searchString, string currentFilter, int? page)
        {
            // Set sorting options
            ViewBag.NameSortParm = sortOrder == "name_desc" ? "name_asc" : "name_desc";
            ViewBag.DateSortParm = sortOrder == "date_desc" ? "date_asc" : "date_desc";

            // Xử lý tìm kiếm
            searchString ??= currentFilter;
            ViewBag.currentFilter = searchString;

            // Lọc và sắp xếp dữ liệu
            var baiBao = context.TableBaiBaos.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                baiBao = baiBao.Where(p => p.TenBaiBao.Contains(searchString));
            }

            baiBao = sortOrder switch
            {
                "name_desc" => baiBao.OrderByDescending(s => s.TenBaiBao),
                "Date" => baiBao.OrderBy(s => s.NgayDangBaiBao),
                "date_desc" => baiBao.OrderByDescending(s => s.NgayDangBaiBao),
                _ => baiBao.OrderBy(s => s.TenBaiBao),
            };

            // Phân trang
            int pageSize = 3;
            int pageNumber = page ?? 1;

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
                binhLuan.NgayBinhLuan = DateTime.Now;
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