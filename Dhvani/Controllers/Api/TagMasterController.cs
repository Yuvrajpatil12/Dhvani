using Core.Entity.Common;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;
using Core.Utility.Common;
using Core.Entity.Enums;
using Core.Resources;
using Core.Business.BusinessFacade;
using Core.Entity;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagMasterController : Controller
    {
        private readonly string _module = "Core.Controllers.App.TagMasterController";
        private JsonMessage _jsonMessage = null;
        private JsonMessageApi _jsonMessageApi = null;

        private JsonMessageDTO jsonResponseMessageDTO = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;
        private string RootPath = ConstantsCommon.RootPath;
        private string AudioPath = ConstantsCommon.AudioPath;
        private string SSMLPath = ConstantsCommon.SSMLPath;
        private string SSMLPathUrl = ConstantsCommon.SSMLPathUrl;
        private string IndiaApiUrl = ConstantsCommon.IndiaApiUrl;
        private string CoreDomain = ConstantsCommon.CoreDomain;

        //private string _userTransactionUUID = null;
        private Int64 _userID = 0;

        public TagMasterController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }


        // write the code for the return the json message jquery datatable
        
        






    }
}