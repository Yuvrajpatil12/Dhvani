using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Entity.ViewModel;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMemoryCache _cache;
        private readonly Helper _helper;
        private readonly string _role;

        private readonly string _module = "Core.Controllers.App.TokenController";

        private JsonMessage _jsonMessage = null;

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public TokenController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment = null, IMemoryCache cache = null)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }

        [HttpGet]
        public string gettoken(string apiKey)
        {
            string strErrorMessage = "Invalid Key";
            string strErrorCode = "";

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, apiKey),
            };

            Users objEntity = new Users();
            if (apiKey == "cms" || apiKey == "cmsssml")
            {
                //get the user details from the session
                string strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";
                if (string.IsNullOrWhiteSpace(strUserID))
                {
                    strErrorCode = "1";
                    strErrorMessage = "1";
                }
            }
            else
            {
                //the request is from api call
                //get the user details from the api key

                UsersBusinessFacade objBF = new UsersBusinessFacade();

                objEntity = objBF.GetUserIdFromAPIKey(apiKey);

                if (objEntity.ID > 0)
                {
                    if (objEntity == null)
                    {
                        strErrorCode = "1";
                        strErrorMessage = "Invalid Key";
                    }
                    else if (objEntity.StatusId == 0)
                    {
                        strErrorCode = "1";
                        strErrorMessage = "User Disabled.";
                    }
                    else if (objEntity.StatusId == 2)
                    {
                        strErrorCode = "1";
                        strErrorMessage = "User Deleted.";
                    }
                }
            }

            if (strErrorCode == "1")
                return strErrorMessage;

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantsCommon.JWT_secret));
            _ = int.TryParse(ConstantsCommon.JWT_expires, out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: ConstantsCommon.JWT_issuer,
                audience: ConstantsCommon.JWT_audience,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            try
            {
                UsersController usersController = new UsersController();
                usersController.InsertAccessMember(objEntity.ID, "GetPronunciation", usersController.getAccessMember());
            }
            catch (Exception ex)
            {
            }

            return tokenString;
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantsCommon.JWT_secret));
            _ = int.TryParse(ConstantsCommon.JWT_expires, out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: ConstantsCommon.JWT_issuer,
                audience: ConstantsCommon.JWT_audience,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //[HttpPost]
        //[Route("update-token")]
        //public JsonResult UpdateToken(APILogin loginModels)
        //{
        //    try
        //    {
        //        LoginVM loginVM = new LoginVM();
        //        Users users = new Users();

        //        if (string.IsNullOrWhiteSpace(loginModels.username) || string.IsNullOrWhiteSpace(loginModels.RefreshToken))
        //        {
        //            _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_emailRefreshTokenRequired, KeyEnums.JsonMessageType.ERROR);
        //        }
        //        else
        //        {
        //            var objUserBusinessFacade = new UsersBusinessFacade();
        //            if (loginModels.RefreshToken != "")
        //            {
        //                UsersBusinessFacade objUsersBusinessFacade = new UsersBusinessFacade();
        //                users = objUsersBusinessFacade.AuthenticateRefreshToken(loginModels.username, loginModels.LoginMode, loginModels.RefreshToken);

        //                if (users != null)
        //                {
        //                    var authClaims = new List<Claim>
        //                       {
        //                           new Claim(ClaimTypes.Name, users.UserName),
        //                           new Claim(JwtRegisteredClaimNames.Jti, users.ID.ToString()),
        //                       };

        //                    var token = CreateToken(authClaims);
        //                    var refreshToken = GenerateRefreshToken();

        //                    users.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        //                    users.RefreshToken = refreshToken;
        //                    users.FCMToken = loginModels.FCMToken;
        //                    users.DeviceID = loginModels.DeviceID;
        //                    objUserBusinessFacade.Save(users);

        //                    _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "", "", users);

        //                }
        //                else
        //                {
        //                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_noRecordsFound, KeyEnums.JsonMessageType.DANGER, "", "");
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(_module, "UpdateToken(APILogin= " + loginModels + ")", ex.Source, ex.Message);
        //        _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : Login(), Source : {0}, Message {1}", ex.Source, ex.Message));
        //    }
        //    return Json(_jsonMessage);
        //}

        //[HttpPost]
        //[Route("get-token")]
        //public JsonResult GetToken(APILogin loginModels)
        //{
        //    var response = JsonConvert.SerializeObject(new { loginModels });
        //    try
        //    {
        //        LoginVM loginVM = new LoginVM();
        //        Users users = new Users();

        //        if (string.IsNullOrWhiteSpace(loginModels.username))
        //        {
        //            _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_eMailAddressRequired, KeyEnums.JsonMessageType.ERROR);
        //        }
        //        else
        //        {
        //            var objUserBusinessFacade = new UsersBusinessFacade();
        //            UsersBusinessFacade objUsersBusinessFacade = new UsersBusinessFacade();
        //            users = objUsersBusinessFacade.GetUserDetailsByUsername(loginModels.username, 2);

        //            if (users != null && (users.RefreshToken == null || users.RefreshToken == ""))
        //            {
        //                var authClaims = new List<Claim>
        //                       {
        //                           new Claim(ClaimTypes.Name, users.UserName),
        //                           new Claim(JwtRegisteredClaimNames.Jti, users.ID.ToString()),
        //                       };

        //                var token = CreateToken(authClaims);
        //                var refreshToken = GenerateRefreshToken();

        //                users.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        //                users.RefreshToken = refreshToken;
        //                users.FCMToken = loginModels.FCMToken;
        //                users.DeviceID = loginModels.DeviceID;
        //                objUserBusinessFacade.Save(users);

        //                _jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "", "", users);

        //            }
        //            else
        //            {
        //                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : Login(), Source : {0}, Message {1}"));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(_module, "GetToken(APILogin= " + loginModels + ")", ex.Source, ex.Message);
        //        _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "", string.Format("Method : Login(), Source : {0}, Message {1}", ex.Source, ex.Message));
        //    }
        //    return Json(_jsonMessage);
        //}
    }
}