using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using websiteTUTHIEN.Models;
using X.PagedList.Extensions;

namespace websiteTUTHIEN.Controllers
{
    public class DuAnController : Controller
    {
        private readonly WebsiteTuthienContext context;
        public DuAnController(WebsiteTuthienContext _context)
        {
            context = _context;
        }
        public IActionResult DuAnIndex()
        {
            return View(); // 
        }
        public IActionResult DuAn()
        {
            // Lấy tất cả bài báo từ cơ sở dữ liệu
            var duans = context.TableDuAns.ToList();

            // Truyền danh sách bài báo vào View
            return View(duans);
        }

        public IActionResult ChiTietDuAn(int id)
        {
            var duan = context.TableDuAns.FirstOrDefault(p => p.MaDuAn == id);
            if (duan == null)
            {
                return NotFound();
            }
            return View(duan);
        }


        //Phân loại theo danh mục, vùng miền, tỉnh thành, có nghiêm trọng hay không?
        //Phân trang, phân loại theo ngày, tháng, năm và tìm kiếm dự án, có phân trang
        public ViewResult IndexDuAn(bool? coNghiemTrong,string currentFilter, int? maDanhMuc, int? maTinhThanh, int? maVungMien, string searchString, int? page)
        {
            var duanQuery = context.TableDuAns.AsQueryable();
            duanQuery = duanQuery.Where(p => p.DaDuyetBai == true);
            if (coNghiemTrong.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.CoNghiemTrong == coNghiemTrong.Value);
            }

            if (maDanhMuc.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.MaDanhMucDa == maDanhMuc.Value);
            }

            if (maTinhThanh.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.MaTinhThanh == maTinhThanh.Value);
            }

            if (maVungMien.HasValue)
            {
                duanQuery = duanQuery.Where(p => context.TableTinhThanhs.Where(t => t.MaVungMien == maVungMien.Value)
                .Select(t => t.MaTinhThanh).Contains(p.MaTinhThanh));
            }
            

            // Tìm kiếm theo tên dự án
            if (!string.IsNullOrEmpty(searchString))
            {
                duanQuery = duanQuery.Where(p => p.TenDuAn.Contains(searchString));
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.DanhMucDuAn = new SelectList(context.TableDanhMucDuAns.ToList(),nameof(TableDanhMucDuAn.MaDanhMucDa),nameof(TableDanhMucDuAn.TenDanhMucDa));
            ViewBag.TinhThanh = new SelectList(context.TableTinhThanhs.ToList(),nameof(TableTinhThanh.MaTinhThanh),nameof(TableTinhThanh.TenTinhThanh));
            ViewBag.VungMien = new SelectList(context.TableVungMiens.ToList(),nameof(TableVungMien.MaVungMien),nameof(TableVungMien.TenVungMien));
            ViewBag.currentFilter = searchString;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(duanQuery.ToPagedList(pageNumber,pageSize));
        }



    }
}
