using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebsiteBeverageDemo.Extension;
using WebsiteBeverageDemo.Helper;
using WebsiteBeverageDemo.Models;
using WebsiteBeverageDemo.ModelViews;

namespace WebsiteBeverageDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly CHBHTHContext _context;
        public INotyfService _notyfService { get; }
        public AccountController(CHBHTHContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        // GET: /<controller>/

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public IActionResult AdminLogin(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("MaNguoiDung");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public async Task<IActionResult> AdminLogin(LoginViewModel kh, string returnUrl = null)
        {

                bool isEmail = Utilities.IsValidEmail(kh.UserName);
                var khachhang = _context.TaiKhoans.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == kh.UserName);

                if (khachhang == null) return RedirectToAction("DangkyTaiKhoan");
                string pass = (kh.Password).ToMD5();
                if (khachhang.Matkhau != pass)
                {
                    _notyfService.Success("Thông tin đăng nhập chưa chính xác");
                    return View(kh);
                }
                //Luu Session MaKh
                HttpContext.Session.SetString("MaNguoiDung", khachhang.MaNguoiDung.ToString());
                var taikhoanID = HttpContext.Session.GetString("MaNguoiDung");

                //Identity
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.HoTen),
                        new Claim("MaNguoiDung", khachhang.MaNguoiDung.ToString())
                    };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                _notyfService.Success("Đăng nhập thành công");
                return RedirectToAction("Index", "Admin");
        }
       
        [Route("logout.html", Name = "Logout")]
        public IActionResult AdminLogout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("MaNguoiDung");
                return RedirectToAction("AdminLogin", "Account", new { Area = "Admin" });
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Account", new { Area = "Admin" });
            }
        }
    }
}
