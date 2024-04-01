using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers
{
    public class SSMLController : Controller
    {
        private readonly string _module = "Core.Controllers.SSMLController";
        private JsonMessage _jsonMessage = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;
        private Int64 _userId = 0;
        private string _role = "";
        private bool isValidUser = false;

        public SSMLController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _role = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) != null ? Convert.ToString(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserRole.ToString())) : "";
            isValidUser = _helper.IsValidUser(_userId, _role, RoleEnums.SuperAdmin + "," + RoleEnums.Admin);
        }

        public IActionResult Index()
        {
            ViewBag.MenuId = KeyEnums.MenuKeys.liSSML.ToString();
            return View();
        }
	}
}
