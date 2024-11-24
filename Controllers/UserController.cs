using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using websiteTUTHIEN.Models;

namespace websiteTUTHIEN.Controllers
{
    public class UserController : Controller
    {
        private readonly WebsiteTuthienContext context;
        public UserController(WebsiteTuthienContext _context)
        {
            context = _context;
        }

        public IActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Dangki(TableNguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await context.TableNguoiDungs.FirstOrDefaultAsync(u => u.TenTk == user.TenTk);
                if (existingUser == null)
                {
                    context.TableNguoiDungs.Add(user);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Tên người dùng đã tồn tại.");
            }
            return View(user);
        }
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await context.TableNguoiDungs.FirstOrDefaultAsync(u => u.TenTk == username && u.MatKhau == password);
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.MaNguoiDung);
                    HttpContext.Session.SetString("Username", user.TenTk);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
            }
            return View();
        }

        public IActionResult Dangxuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult EditProfile()
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

            return View(user); // Trả về view để người dùng chỉnh sửa thông tin
        }

        // Action để xử lý việc cập nhật thông tin cá nhân
        [HttpPost]
        public async Task<IActionResult> EditProfile(TableNguoiDung updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = await context.TableNguoiDungs.FindAsync(updatedUser.MaNguoiDung);
                if (user == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin người dùng
                user.TenNguoiDung = updatedUser.TenNguoiDung;
                user.AvatarNguoiDung = updatedUser.AvatarNguoiDung; // Nếu cần
                user.MatKhau = updatedUser.MatKhau; // Cần thận trọng với việc cập nhật mật khẩu
                user.SdtnguoiDung = updatedUser.SdtnguoiDung;
                user.Email = updatedUser.Email;
                user.DiaChi = updatedUser.DiaChi;
                user.NamSinh = updatedUser.NamSinh;

                await context.SaveChangesAsync();
                return RedirectToAction("Index"); // Chuyển hướng về trang chính sau khi cập nhật
            }
            return View(updatedUser); // Nếu có lỗi, trả về view với thông tin đã nhập
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

    }
}