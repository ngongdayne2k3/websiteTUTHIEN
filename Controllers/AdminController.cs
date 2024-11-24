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
        public async Task<IActionResult> EditDanhMucDUAn(int id, TableDanhMucDuAn danhMucDuAn)
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

        //------------------------------------------------------VungMien
        public async Task<IActionResult> IndexVM()
        {
            return View(await context.TableVungMiens.ToListAsync());
        }

        public IActionResult CreateVM()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVM(TableVungMien vungMien)
        {
            if (ModelState.IsValid)
            {
                context.TableVungMiens.Add(vungMien);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexVM));
            }
            return View(vungMien);
        }

        public async Task<IActionResult> EditVM(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vungMien = await context.TableVungMiens.FindAsync(id);
            if (vungMien == null)
            {
                return NotFound();
            }
            return View(vungMien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVM(int id, TableVungMien vungMien)
        {
            if (id != vungMien.MaVungMien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.TableVungMiens.Update(vungMien);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableVungMiens.Any(e => e.MaVungMien == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexVM));
            }
            return View(vungMien);
        }

        public async Task<IActionResult> DeleteVM(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vungMien = await context.TableVungMiens.FirstOrDefaultAsync(m => m.MaVungMien == id);
            if (vungMien == null)
            {
                return NotFound();
            }
            return View(vungMien);
        }

        [HttpPost, ActionName("DeleteVM")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteVM(int id)
        {
            var vungMien = await context.TableVungMiens.FindAsync(id);
            context.TableVungMiens.Remove(vungMien);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexVM));
        }

        //------------------------------------------------------MucDo
        public async Task<IActionResult> IndexMD()
        {
            return View(await context.TableMucDoDuAns.ToListAsync());
        }

        public IActionResult CreateMD()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMD(TableMucDoDuAn mucDoDuAn)
        {
            if (ModelState.IsValid)
            {
                context.TableMucDoDuAns.Add(mucDoDuAn);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexMD));
            }
            return View(mucDoDuAn);
        }

        public async Task<IActionResult> EditMD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mucDoDuAn = await context.TableMucDoDuAns.FindAsync(id);
            if (mucDoDuAn == null)
            {
                return NotFound();
            }
            return View(mucDoDuAn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMD(int id, TableMucDoDuAn mucDoDuAn)
        {
            if (id != mucDoDuAn.MaMucDoDuAn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.TableMucDoDuAns.Update(mucDoDuAn);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableMucDoDuAns.Any(e => e.MaMucDoDuAn == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexMD));
            }
            return View(mucDoDuAn);
        }

        public async Task<IActionResult> DeleteMD(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mucDoDuAn = await context.TableMucDoDuAns.FirstOrDefaultAsync(m => m.MaMucDoDuAn == id);
            if (mucDoDuAn == null)
            {
                return NotFound();
            }
            return View(mucDoDuAn);
        }

        [HttpPost, ActionName("DeleteMD")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteMD(int id)
        {
            var mucDoDuAn = await context.TableMucDoDuAns.FindAsync(id);
            context.TableMucDoDuAns.Remove(mucDoDuAn);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexMD));
        }

        //------------------------------------------------------TrangThai
        public async Task<IActionResult> IndexTT()
        {
            return View(await context.TableTrangThais.ToListAsync());
        }

        public IActionResult CreateTT()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTT(TableTrangThai trangThai)
        {
            if (ModelState.IsValid)
            {
                context.TableTrangThais.Add(trangThai);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexTT));
            }
            return View(trangThai);
        }

        public async Task<IActionResult> EditTT(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trangThai = await context.TableTrangThais.FindAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTT(int id, TableTrangThai trangThai)
        {
            if (id != trangThai.MaTrangThai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.TableTrangThais.Update(trangThai);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableTrangThais.Any(e => e.MaTrangThai == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexMD));
            }
            return View(trangThai);
        }

        public async Task<IActionResult> DeleteTT(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trangThai = await context.TableTrangThais.FirstOrDefaultAsync(m => m.MaTrangThai == id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }

        [HttpPost, ActionName("DeleteTT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteTT(int id)
        {
            var trangThai = await context.TableTrangThais.FindAsync(id);
            context.TableTrangThais.Remove(trangThai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexTT));
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