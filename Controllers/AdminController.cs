using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using websiteTUTHIEN.Models;
using X.PagedList.Extensions;

namespace websiteTUTHIEN.Controllers
{
    public class AdminController : Controller
    {
        private readonly WebsiteTuthienContext context;
        private readonly IWebHostEnvironment env;
        public AdminController(WebsiteTuthienContext _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Danh Mục dự án
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
        #endregion

        #region  Người dùng
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
                return RedirectToAction(nameof(IndexND));
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
            return RedirectToAction(nameof(IndexND));
        }
        #endregion

        #region Bài Báo
        //------------------Hiển thị bài báo
        public IActionResult CreateBaiBao()
        {
            ViewBag.MaDanhMucBaiBao = new SelectList(context.TableDanhMucBaiBaos.ToList(), nameof(TableDanhMucBaiBao.MaDanhMucBaiBao), nameof(TableDanhMucBaiBao.TenDanhMucBaiBao));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBaiBao(TableBaiBao baiBao, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(env.WebRootPath, "images", "baibao", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    baiBao.HinhanhBaiBao = "../images/baibao/" + fileName;
                }
                baiBao.NgayDangBaiBao = DateTime.Now;
                context.TableBaiBaos.Add(baiBao);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexBaiBao));
            }
            ViewBag.MaDanhMucBaiBao = new SelectList(context.TableDanhMucBaiBaos.ToList(), nameof(TableDanhMucBaiBao.MaDanhMucBaiBao), nameof(TableDanhMucBaiBao.TenDanhMucBaiBao));
            return View(baiBao);
        }

        public IActionResult IndexBaiBao()
        {
            var baiBao = context.TableBaiBaos.ToList();
            return View(baiBao);
        }

        public async Task<IActionResult> EditBaiBao(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            var baiBao = await context.TableBaiBaos.FindAsync(id);
            ViewData["DanhMucBaiBao"] = new SelectList(context.TableDanhMucBaiBaos, "MaDanhMucBaiBao", "TenDanhMucBaiBao");
            if (baiBao == null)
            {
                NotFound();
            }
            return View(baiBao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBaiBao(int id, TableBaiBao baiBao, IFormFile imageFile)
        {
            if (baiBao.MaBaiBao != id)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(env.WebRootPath, "images", "baibao", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        baiBao.HinhanhBaiBao = "../images/baibao/" + fileName;
                    }
                    baiBao.NgayDangBaiBao = DateTime.Now;
                    context.TableBaiBaos.Update(baiBao);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableBaiBaos.Any(e => e.MaDanhMucBaiBao == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexBaiBao));
            }
            return View(baiBao);
        }

        public async Task<IActionResult> DeleteBaiBao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var baiBao = await context.TableBaiBaos.FirstOrDefaultAsync(m => m.MaBaiBao == id);
            if (baiBao == null)
            {
                return NotFound();
            }
            return View(baiBao);
        }

        [HttpPost, ActionName("DeleteBaiBao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteBaiBao(int id)
        {
            var baiBao = await context.TableBaiBaos.FindAsync(id);
            context.TableBaiBaos.Remove(baiBao);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexBaiBao));
        }
        #endregion

        #region Danh mục bài báo
        //-----------------------------Danh mục bài báo
        public async Task<IActionResult> IndexDanhMucBaiBao()
        {
            return View(await context.TableDanhMucBaiBaos.ToListAsync());
        }

        public IActionResult CreateDanhMucBaiBao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDanhMucBaiBao(TableDanhMucBaiBao danhMucBaiBao)
        {
            if (ModelState.IsValid)
            {
                context.TableDanhMucBaiBaos.Add(danhMucBaiBao);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexDanhMucBaiBao));
            }
            return View(danhMucBaiBao);
        }

        public async Task<IActionResult> EditDanhMucBaiBao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhMucBaiBao = await context.TableDanhMucBaiBaos.FindAsync(id);
            if (danhMucBaiBao == null)
            {
                return NotFound();
            }
            return View(danhMucBaiBao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDanhMucBaiBao(int id, TableDanhMucBaiBao danhMucBaiBao)
        {
            if (id != danhMucBaiBao.MaDanhMucBaiBao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.TableDanhMucBaiBaos.Update(danhMucBaiBao);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableDanhMucBaiBaos.Any(e => e.MaDanhMucBaiBao == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDanhMucBaiBao));
            }
            return View(danhMucBaiBao);
        }

        public async Task<IActionResult> DeleteDanhMucBaiBao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhMucBaiBao = await context.TableDanhMucBaiBaos.FirstOrDefaultAsync(m => m.MaDanhMucBaiBao == id);
            if (danhMucBaiBao == null)
            {
                return NotFound();
            }
            return View(danhMucBaiBao);
        }

        [HttpPost, ActionName("DeleteDanhMucBaiBao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteDanhMucBaiBao(int id)
        {
            var danhMucBaiBao = await context.TableDanhMucBaiBaos.FindAsync(id);
            context.TableDanhMucBaiBaos.Remove(danhMucBaiBao);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexDanhMucBaiBao));
        }

        #endregion
        #region Dự án
        //Cần thêm cái phần IndexDuAn(có sắp xếp, tìm kiếm, phân trang)
        public ViewResult IndexDuAn(string sortOrder, string searchString, string currentFilter, int? page)
        {
            // Set sorting options
            ViewBag.NameSortParm = sortOrder == "name_desc" ? "name_asc" : "name_desc";
            ViewBag.DateSortParm = sortOrder == "date_desc" ? "date_asc" : "date_desc";

            // Xử lý tìm kiếm
            searchString ??= currentFilter;
            ViewBag.currentFilter = searchString;

            // Lọc và sắp xếp dữ liệu
            var duAns = context.TableDuAns.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                duAns = duAns.Where(p => p.TenDuAn.Contains(searchString));
            }

            duAns = sortOrder switch
            {
                "name_desc" => duAns.OrderByDescending(s => s.TenDuAn),
                _ => duAns.OrderBy(s => s.TenDuAn),
            };

            // Phân trang
            int pageSize = 6;
            int pageNumber = page ?? 1;

            return View(duAns.ToPagedList(pageNumber, pageSize));
        }
        //Thêm cái phần sửa dự án, xoá dự án
        //Thêm phần duyệt dự án
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveDuAn(int id)
        {
            var duAn = await context.TableDuAns.FindAsync(id);
            if (duAn == null)
            {
                return NotFound();
            }

            duAn.DaDuyetBai = true; // Đánh dấu là đã duyệt
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexDuAn));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectDuAn(int id)
        {
            var duAn = await context.TableDuAns.FindAsync(id);
            if (duAn == null)
            {
                return NotFound();
            }

            duAn.DaDuyetBai = false; // Đánh dấu là không duyệt
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexDuAn));
        }
        #endregion
    }
}