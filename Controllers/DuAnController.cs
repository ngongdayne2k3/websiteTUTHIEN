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
            return View(await context.TableDuAns.Where(p=> p.DaDuyetBai == false).ToListAsync());
        }

        public IActionResult ChiTietDuAn(int id){
            var duan = context.TableDuAns.FirstOrDefault(p=> p.MaDanhMucDa == id);
            if (duan == null){
                return NotFound();
            }
            return View(duan);
        }

        
        
    }
}
