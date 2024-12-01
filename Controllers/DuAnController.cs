﻿using Microsoft.AspNetCore.Mvc;
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


        public IActionResult ChiTietDuAn(int id){
            var duan = context.TableDuAns.FirstOrDefault(p=> p.MaDanhMucDa == id);
            if (duan == null){
                return NotFound();
            }
            return View(duan);
        }

        public async Task<IActionResult> IndexDuAn(bool? coNghiemTrong, int? maDanhMuc, int? maTinhThanh, int? maVungMien){
            var duanQuery = context.TableDuAns.AsQueryable();

            duanQuery = duanQuery.Where(p=>p.DaDuyetBai == true);
            if(coNghiemTrong.HasValue){
                duanQuery = duanQuery.Where(p => p.CoNghiemTrong == coNghiemTrong.Value);
            }

            if(maDanhMuc.HasValue){
                duanQuery = duanQuery.Where(p => p.MaDanhMucDa == maDanhMuc.Value);
            }

            if(maTinhThanh.HasValue){
                duanQuery = duanQuery.Where(p => p.MaTinhThanh == maTinhThanh.Value);
            }

            if(maVungMien.HasValue){
                duanQuery = duanQuery.Where(p => context.TableTinhThanhs.Where(t => t.MaVungMien == maVungMien.Value)
                .Select(t => t.MaTinhThanh).Contains(p.MaTinhThanh));
            }
            return View(await duanQuery.ToListAsync());
        }
        
    }
}
