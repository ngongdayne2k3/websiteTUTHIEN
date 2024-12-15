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
            return View();
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
                nguoiDung.AvatarNguoiDung = "../images/avatar.png";
                try
                {
                    // Thêm người dùng vào cơ sở dữ liệu
                    context.TableNguoiDungs.Add(nguoiDung);
                    await context.SaveChangesAsync();

                    // Lưu thông tin người dùng vào session
                    HttpContext.Session.SetString("UserId", nguoiDung.MaNguoiDung.ToString());
                    HttpContext.Session.SetString("TenTK", nguoiDung.TenTk);

                    // Chuyển hướng về trang chủ
                    return RedirectToAction("DangNhap", "User");
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
        public IActionResult DangNhap(TableNguoiDung u)
        {
            // Find the user by username
            var user = context.TableNguoiDungs.FirstOrDefault(x => x.TenTk.Equals(u.TenTk));

            if (user != null)
            {
                // Verify the password
                if (BCrypt.Net.BCrypt.Verify(u.MatKhau, user.MatKhau))
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    // Store only necessary user information in the session
                    HttpContext.Session.SetInt32("MaTK", user.MaNguoiDung);
                    HttpContext.Session.SetString("TenTk", user.TenTk);
                    HttpContext.Session.SetString("TenND", user.TenNguoiDung);
                    HttpContext.Session.SetString("HinhAnh", user.AvatarNguoiDung);// Assuming you have a property for the user's name
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Password is incorrect
                    ModelState.AddModelError("", "Mật khẩu không chính xác.");
                }
            }
            else
            {
                // User not found
                ModelState.AddModelError("", "Đăng Nhập Thất Bại! Kiểm Tra Lại Thông Tin Đăng Nhập");
            }

            // If we reach here, something went wrong, return to the login view with errors
            return View(u); // Return the view with the model to show validation errors
        }


        public IActionResult Dangxuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
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
                var userId = HttpContext.Session.GetInt32("MaTK");
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
            return View(duAn);
        }

        public async Task<IActionResult> IndexDuAnUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var products = await context.TableDuAns
            .Where(p => p.MaNguoiDung == userId)
            .ToListAsync();
            return View(products);
        }
    }
}