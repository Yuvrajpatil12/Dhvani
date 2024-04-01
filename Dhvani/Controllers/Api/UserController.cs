using Core.Entity;
using Core.Entity.Enums;
using Core.Resources;
using Core.Utility.Common;
using Core.Entity.Common;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Core.Business.BusinessFacade;
using Core.Entity.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;

namespace Dhvani.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly string _module = "Dhvani.Controllers.Api.UserController";
        private JsonMessage _jsonMessage = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IMemoryCache _cache;
        private Helper _helper;

        public UserController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _helper = new Helper(_httpContextAccessor);
        }

        #region "Login"

        [HttpPost]
        [Route("Login")]
        [SwaggerOperation("Login API", "User Login with users schema")]
        public JsonResult Login(APILogin loginModels)
        {
            var revsponse = JsonConvert.SerializeObject(new { loginModels });
            Log.WriteLog(_module, "Login(APILogin= " + loginModels + ")", revsponse, "Request Recived");
            try
            {
                LoginVM loginVM = new LoginVM();
                Users objUserEntity = new Users();
                if (string.IsNullOrWhiteSpace(loginModels.username) || string.IsNullOrWhiteSpace(loginModels.password))
                {
                    _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_emailPasswordRequired, KeyEnums.JsonMessageType.ERROR);
                }
                else
                {
                    loginModels.password = new Encription().Encrypt(loginModels.password);

                    var objUserBusinessFacade = new UsersBusinessFacade();

                    _jsonMessage = new UsersController().IsLoginValid(loginModels.username, loginModels.password, "APP");

                    if (_jsonMessage.IsSuccess)
                    {
                        objUserEntity = (Users)_jsonMessage.Data;

                        new UsersController().InsertAccessMember(objUserEntity.ID, "Login", loginModels.AccessMember);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Login(APILogin= " + loginModels + ")", ex.Source, ex.Message);
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "Ausnahme", string.Format("Method : Login(), Source : {0}, Message {1}", ex.Source, ex.Message));
            }

            return Json(_jsonMessage);
        }

        #endregion "Login"

        #region "ForgotPassword"

        [HttpPost]
        [Route("ForgotPassword")]
        [SwaggerOperation("User ForgotPassword API", "API will trigger an email on resgistered email id after validating user with status and username")]
        public dynamic ForgotPassword(string UserName)
        {
            var response = string.Empty;
            var username = UserName;//ObjectUser == null ? "" : ObjectUser.UserName;
            var revsponse = JsonConvert.SerializeObject(new { UserName = username });
            Log.WriteInfoLogWithoutMail(_module, "ForgotPassword(mstObject=" + UserName + ")", "INFO LOG ", revsponse);
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var Success = false;
                    var message = "";
                    int i = 1;
                    var objUserEntity1 = new UsersController(_hostingEnvironment, _httpContextAccessor, _cache).ForgotPassword(username);
                         
                    

                    var objdynamicallObject = new dynamicallObject();
                    objdynamicallObject.isSuccess = Success;
                    objdynamicallObject.Message = message;

                    response = JsonConvert.SerializeObject(new { Response = objdynamicallObject });
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "ForgotPassword(username=" + username + ")", ex.Source, ex.Message);
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(response, System.Text.Encoding.UTF8, "application/json")
            };
        }

        #endregion "ForgotPassword"

        #region "GeneratedToke"
        [HttpPost("generate-token")]
        public IActionResult RegenerateToken([FromBody] long userId)
        {
            var requestData = JsonConvert.SerializeObject(new { userId });
           

            try
            {
                Log.WriteInfoLogWithoutMail(_module, "RegenerateToken(Test return uuid )", "INFOLOG ", requestData);
            }
            catch (Exception ex)
            {
            }

            // Call the stored procedure to generate a new token
            Console.WriteLine($"user id :" + userId);
            RandomStringGenerator randomStringGenerator = new RandomStringGenerator();
            //var apiKeyRefreshToken  = randomStringGenerator.APIKeyRefreshToken();
       
            var apiKeyRefreshToken = Guid.NewGuid();
            UsersBusinessFacade objUserBusinessFacade = new UsersBusinessFacade();
            Users users = new Users();

            if (userId > 0)
            {
                users.APIKey = Convert.ToString(apiKeyRefreshToken);
                users.ID = userId;
                objUserBusinessFacade.Save(users);
            }
            return Ok(new { Token = apiKeyRefreshToken });
        }
        #endregion

        #region "AI drop down values"

        // user input base find the values in lique query and return the new values for the drop down
        [HttpGet]
        [Route("GetDropDownValues")]
        [SwaggerOperation("GetDropDownValues API", "GetDropDownValues API")]
        public JsonResult GetDropDownValues()
        {
            try
            {
                var objUserBusinessFacade = new VoiceMasterBusinessFacade();
                var dropdownValues = objUserBusinessFacade.GetDropDownValues(); // Adjust this method accordingly
                return Json(dropdownValues); // Return dropdown values
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDropDownValues", ex.Source, ex.Message);
                var errorMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "Ausnahme", string.Format("Method : GetDropDownValues(), Source : {0}, Message {1}", ex.Source, ex.Message));
                return Json(errorMessage); // Return error message
            }
        }

        #endregion

        #region "AI drop down values vase on LocaleName"

        [HttpGet]
        [Route("GetLanguageRelatedValues")]
        [SwaggerOperation("GetLanguageRelatedValues API", "GetLanguageRelatedValues API")]
        public JsonResult GetLanguageRelatedValues(string language)
        {
            try
            {
                var objUserBusinessFacade = new VoiceMasterBusinessFacade();
                var relatedValues = objUserBusinessFacade.GetLanguageRelatedValues(language); // Adjust this method accordingly
                return Json(relatedValues); // Return related values
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues", ex.Source, ex.Message);
                var errorMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "Ausnahme", string.Format("Method : GetRelatedValues(), Source : {0}, Message {1}", ex.Source, ex.Message));
                return Json(errorMessage); // Return error message
            }
        }


        #endregion

        // write the code for the post method for testing purpose call back url withou any parameter or logic   
        [HttpPost]
        [Route("PostApi")]
        [SwaggerOperation("PostApi API", "PostApi API")]
        public JsonResult PostApi()
        {
            try
            {
              
                return Json(string.Empty); 
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "PostApi", ex.Source, ex.Message);
                var errorMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", "Ausnahme", string.Format("Method : PostApi(), Source : {0}, Message {1}", ex.Source, ex.Message));
                return Json(errorMessage); // Return error message
            }
        }
            

        
      

    }
}