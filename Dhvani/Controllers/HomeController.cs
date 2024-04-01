using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _module = "Core.Controllers.HomeController";
        private JsonMessage _jsonMessage = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;
        private Int64 _userId = 0;
        private string _role = "";
        private bool isValidUser = false;

        public HomeController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.User);
            _hostingEnvironment = environment;
        }

        
        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liHome.ToString();

            return View();
        }


    }
}