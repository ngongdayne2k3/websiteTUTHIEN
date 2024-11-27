using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class AdminControllers : Controller
    {
        private readonly WebsiteTuthienContext context;
        public AdminControllers(WebsiteTuthienContext _context)
        {
            context = _context;
        }

        //DanhMucDuAn
        //GET: Admin/IndexDanhMucDuAn
        public async Task<IActionResult> IndexDanhMucDuAn()
        {
            return View(await context.TableDanhMucDuAns.ToListAsync());
        }

        //GET: Admin/CreateDanhMucDuAn
        public IActionResult CreateDanhMucDuAn()
        {
            return View();
        }

        //POST : Admin/CreateDanhMucDuAn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDanhMucDuAn(TableDanhMucDuAn danhMucDuAn)
        {
            if (ModelState.IsValid)
            {
                context.TableDanhMucDuAns.Add(danhMucDuAn);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexDanhMucDuAn));
            }
            return View(danhMucDuAn);
        }

        //GET: Admin/EditDanhMucDuAn/5
        public async Task<IActionResult> EditDanhMucDuAn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhMucDuAn = await context.TableDanhMucDuAns.FindAsync(id);
            if (danhMucDuAn == null)
            {
                return NotFound();
            }
            return View(danhMucDuAn);
        }

        //POST: Admin/EditDanhMucDuAn/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDanhMucDuAn(int id, TableDanhMucDuAn danhMucDuAn)
        {
            if (id != danhMucDuAn.MaDanhMucDa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.TableDanhMucDuAns.Update(danhMucDuAn);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableDanhMucDuAns.Any(e => e.MaDanhMucDa == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDanhMucDuAn));
            }
            return View(danhMucDuAn);
        }

        public async Task<IActionResult> DeleteDanhMucDuAn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhMucDuAn = await context.TableDanhMucDuAns.FirstOrDefaultAsync(m => m.MaDanhMucDa == id);
            if (danhMucDuAn == null)
            {
                return NotFound();
            }
            return View(danhMucDuAn);
        }

        [HttpPost, ActionName("DeteleDanhMucDuAn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteDanhMucDuAn(int id)
        {
            var danhMucDuAn = await context.TableDanhMucDuAns.FindAsync(id);
            context.TableDanhMucDuAns.Remove(danhMucDuAn);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexDanhMucDuAn));
        }

        //-------------------------------NguoiDung
        public async Task<IActionResult> IndexND()
        {
            return View(await context.TableNguoiDungs.ToListAsync());
        }

        //Chinh sua mot doi tuong tu admin
        public IActionResult Chinhsua(int id)
        {
            var user = context.TableNguoiDungs.FirstOrDefault(u => u.MaNguoiDung == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Chinhsua(TableNguoiDung updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = await context.TableNguoiDungs.FindAsync(updatedUser.MaNguoiDung);
                if (user == null)
                {
                    return NotFound();
                }

                user.TenNguoiDung = updatedUser.TenNguoiDung;
                user.AvatarNguoiDung = updatedUser.AvatarNguoiDung;
                user.TenTk = updatedUser.TenTk;
                user.MatKhau = updatedUser.MatKhau;
                user.SdtnguoiDung = updatedUser.SdtnguoiDung;
                user.Email = updatedUser.Email;
                user.DiaChi = updatedUser.DiaChi;
                user.NamSinh = updatedUser.NamSinh;
                await context.SaveChangesAsync();
                return RedirectToAction("ListNguoiDung");
            }
            return View(updatedUser);
        }

        //xoa doi tuong tren admin
        public IActionResult Delete(int id)
        {
            var user = context.TableNguoiDungs.FirstOrDefault(u => u.MaNguoiDung == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await context.TableNguoiDungs.FindAsync(id);
            if (user != null)
            {
                context.TableNguoiDungs.Remove(user);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("ListNguoiDung");
        }
    }
}