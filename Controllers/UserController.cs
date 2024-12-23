using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class UserController : Controller
    {
        private readonly WebsiteTuthienContext context;
        private readonly IWebHostEnvironment env;
        public UserController(WebsiteTuthienContext _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }

        public IActionResult NguoiDung()
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaTK");
            if (maNguoiDung == null)
            {
                return RedirectToAction("DangNhap", "User"); // Chuyển hướng đến trang đăng nhập nếu Session không tồn tại
            }

            // Truy vấn thông tin người dùng từ cơ sở dữ liệu
            var user = context.TableNguoiDungs.FirstOrDefault(u => u.MaNguoiDung == maNguoiDung);
            if (user == null)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            // Trả về View và truyền model người dùng
            return View(user);
        }

        public IActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangKi([Bind("TenNguoiDung,Email,TenTk,MatKhau")] TableNguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Xử lý chuỗi để đảm bảo không lỗi khi so sánh
                var emailInput = nguoiDung.Email.Trim().ToLower();
                var usernameInput = nguoiDung.TenTk.Trim();

                // Kiểm tra email đã tồn tại
                var checkEmail = await context.TableNguoiDungs
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == emailInput);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("", "Địa chỉ Email đã tồn tại");
                    return View();
                }

                // Kiểm tra tên tài khoản đã tồn tại
                var checkTK = await context.TableNguoiDungs
                    .FirstOrDefaultAsync(x => x.TenTk == usernameInput);
                if (checkTK != null)
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại");
                    return View();
                }

                // Mã hóa mật khẩu (dùng BCrypt)
                nguoiDung.MatKhau = BCrypt.Net.BCrypt.HashPassword(nguoiDung.MatKhau);

                //Gán avatar mặc định vào khi tạo tài khoản mới
                nguoiDung.AvatarNguoiDung =  "../images/avatar.png";
                
                try
                {
                    // Thêm người dùng vào cơ sở dữ liệu
                    context.TableNguoiDungs.Add(nguoiDung);
                    await context.SaveChangesAsync();

                    // Lưu thông tin người dùng vào session
                    HttpContext.Session.SetString("UserId", nguoiDung.MaNguoiDung.ToString());
                    HttpContext.Session.SetString("TenTK", nguoiDung.TenTk);

                    // Chuyển hướng về trang chủ
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                    return View();
                }
            }

            // Trả lại view nếu model không hợp lệ
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(string TenTk, string Matkhau,TableNguoiDung u,TableAdmin a)
        {
            // Kiểm tra trong bảng admin trước
            var admin = context.TableAdmins.FirstOrDefault(x => x.TenTk.Equals(a.TenTk));

            if (admin != null)
            {
                // So sánh mật khẩu mã hóa
                if (BCrypt.Net.BCrypt.Verify(a.Matkhau, admin.Matkhau))
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    // Đăng nhập thành công với tài khoản admin
                    HttpContext.Session.SetInt32("MaAdmin", admin.MaAdmin);
                    HttpContext.Session.SetString("TenTk", admin.TenTk);
                    HttpContext.Session.SetString("TenAdmin", admin.Tenadmin);
                    HttpContext.Session.SetString("Role", "Admin"); // Lưu vai trò admin
                    ViewBag.Thongbao = "Đăng nhập thành công với quyền Admin.";
                    return RedirectToAction("IndexNd", "Admin");
                }
                else
                {
                    // Mật khẩu không chính xác
                    ModelState.AddModelError("", "Mật khẩu không chính xác.");
                }
            }
            else
            {
                // Nếu không phải admin, kiểm tra người dùng
                var user = context.TableNguoiDungs.FirstOrDefault(x => x.TenTk.Equals(u.TenTk));

                if (user != null && BCrypt.Net.BCrypt.Verify(u.MatKhau, user.MatKhau))
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    // Đăng nhập thành công với tài khoản người dùng
                    HttpContext.Session.SetInt32("MaTK", user.MaNguoiDung);
                    HttpContext.Session.SetString("TenTk", user.TenTk);
                    HttpContext.Session.SetString("TenND", user.TenNguoiDung);
                    HttpContext.Session.SetString("HinhAnh", user.AvatarNguoiDung);// Assuming you have a property for the user's name
                    HttpContext.Session.SetString("Role", "User"); // Lưu vai trò người dùng
                    ViewBag.Thongbao = "Đăng nhập thành công.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Sai tên tài khoản hoặc mật khẩu
                    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không chính xác.");
                }
            }

            // Nếu có lỗi, trả lại view đăng nhập
            return View();
        }

        public IActionResult Dangxuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }

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
                return RedirectToAction(nameof(NguoiDung));
            }
            return View(updatedUser);
        }

        public IActionResult DeleteAccount()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("DangNhap"); // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
            }

            var user = context.TableNguoiDungs.FirstOrDefault(u => u.MaNguoiDung == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Trả về view để người dùng xác nhận xóa tài khoản
        }

        // Action để xử lý việc xóa tài khoản
        [HttpPost, ActionName("DeleteAccount")]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("DangNhap"); // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
            }

            var user = await context.TableNguoiDungs.FindAsync(userId);
            if (user != null)
            {
                context.TableNguoiDungs.Remove(user);
                await context.SaveChangesAsync();
                HttpContext.Session.Clear(); // Xóa thông tin phiên làm việc
                return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chính
            }
            return NotFound();
        }

        public IActionResult CreateDuAn()
        {
            ViewData["DanhMucDa"] = new SelectList(context.TableDanhMucDuAns, "MaDanhMucDa", "TenDanhMucDa");
            ViewData["TinhThanh"] = new SelectList(context.TableTinhThanhs, "MaTinhThanh", "TenTinhThanh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDuAn(TableDuAn duAn, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    duAn.Hinhanh = "/images/duan/" + fileName;
                }
                var userId = HttpContext.Session.GetInt32("UserId");
                duAn.MaNguoiDung = userId ?? 0;
                duAn.DaDuyetBai = false;
                duAn.DaKetThucDuAn = false;
                duAn.SoTienHienTai = 0;
                context.Add(duAn);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexDuAnUser));
            }
            ViewData["MaDanhMucDa"] = new SelectList(context.TableDanhMucDuAns, "MaDanhMucDa", "TenDanhMucDa");
            ViewData["MaTinhThanh"] = new SelectList(context.TableTinhThanhs, "MaTinhThanh", "TenTinhThanh");
            return View(User);
        }

        public async Task<IActionResult> IndexDuAnUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var products = await context.TableDuAns
            .Where(p => p.MaNguoiDung == userId)
            .ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> EditDuAn(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            var duAn = await context.TableDuAns.FindAsync(id);
            ViewBag.DanhMucDuAn = new SelectList(context.TableDanhMucDuAns, nameof(TableDanhMucDuAn.MaDanhMucDa), nameof(TableDanhMucDuAn.TenDanhMucDa));
            ViewBag.TinhThanh = new SelectList(context.TableTinhThanhs, nameof(TableTinhThanh.MaTinhThanh), nameof(TableTinhThanh.TenTinhThanh));
            if (duAn == null)
            {
                NotFound();
            }
            return View(duAn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDuAn(int id, TableDuAn duAn, IFormFile imageFile)
        {
            if (duAn.MaDuAn != id)
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
                        var filePath = Path.Combine(env.WebRootPath, "images", "duan", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        duAn.Hinhanh = "../images/duan/" + fileName;
                    }
                    duAn.DaDuyetBai = false;
                    context.TableDuAns.Update(duAn);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.TableDuAns.Any(e => e.MaDuAn == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexDuAnUser));
            }
            ViewBag.DanhMucDuAn = new SelectList(context.TableDanhMucDuAns, nameof(TableDanhMucDuAn.MaDanhMucDa), nameof(TableDanhMucDuAn.TenDanhMucDa));
            ViewBag.TinhThanh = new SelectList(context.TableTinhThanhs, nameof(TableTinhThanh.MaTinhThanh), nameof(TableTinhThanh.TenTinhThanh));
            return View(duAn);
        }
    }
}