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
        public DuAnController(WebsiteTuthienContext _context)
        {
            context = _context;
        }

        public IActionResult ChiTietDuAn(int id)
        {
            var duan = context.TableDuAns.FirstOrDefault(p => p.MaDanhMucDa == id);
            if (duan == null)
            {
                return NotFound();
            }
            return View(duan);
        }


        //Phân loại theo danh mục, vùng miền, tỉnh thành, có nghiêm trọng hay không?
        //Phân trang, phân loại theo ngày, tháng, năm và tìm kiếm dự án, thiếu phân trang?
        public async Task<IActionResult> IndexDuAn(bool? coNghiemTrong, int? maDanhMuc, int? maTinhThanh, int? maVungMien, int? ngay, int? thang, int? nam, string tenDuAn)
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
            // Lọc theo ngày, tháng, năm
            if (ngay.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.Ngaybatdau.Day == ngay.Value);
            }

            if (thang.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.Ngaybatdau.Month == thang.Value);
            }

            if (nam.HasValue)
            {
                duanQuery = duanQuery.Where(p => p.Ngaybatdau.Year == nam.Value);
            }

            // Tìm kiếm theo tên dự án
            if (!string.IsNullOrEmpty(tenDuAn))
            {
                duanQuery = duanQuery.Where(p => p.TenDuAn.Contains(tenDuAn));
            }
            return View(await duanQuery.ToListAsync());
        }



    }
}
