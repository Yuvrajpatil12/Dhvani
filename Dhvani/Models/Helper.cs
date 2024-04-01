using Core.Entity;
using Core.Entity.Enums;
using Core.Models;
using Core.Utility;
using Core.Utility.Common;
using Core.Entity.ViewModel;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Core.Models
{
    public class Helper
    {
        private readonly string _module = "Core.Models.Helper";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private bool _IsRegisterProcess = false;

        public bool IsRegisterProcess
        {
            get { return _IsRegisterProcess; }
            set { _IsRegisterProcess = value; }
        }

        public void KillSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public LoginVM GetSession()
        {
            LoginVM loginVM = new LoginVM();

            try
            {
                if (_session.GetString(KeyEnums.SessionKeys.UserSession.ToString()) != null)
                {
                    loginVM = JsonConvert.DeserializeObject<LoginVM>(_session.GetString(KeyEnums.SessionKeys.UserSession.ToString()));
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetSession()", ex.Source, ex.Message, ex);
            }
            return loginVM;
        }

        public void SetSession(Users user)
        {
            try
            {
                if (user != null)
                {
                    LoginVM loginVM = new LoginVM();
                    loginVM.SessionId = _session.Id;

                    loginVM.Id = user.ID;
                    loginVM.UserName = user.UserName;
                    loginVM.Password = user.Password;
                    loginVM.FirstName = user.FirstName;
                    loginVM.RoleId = (byte)user.RoleId;
                    loginVM.ParentId = user.ParentId;

                    loginVM.IsEmailVerified = user.IsEmailVerified;
                    loginVM.EmailVerficationCode = user.EmailVerficationCode;
                    
                    loginVM.StatusId = user.StatusId;
                    loginVM.CreatedDate = user.CreatedDate;
                    
                    loginVM.LanguageId = user.LanguageId;
                    loginVM.Profile_Logo = user.ProfilePicture;

                    if (IsRegisterProcess)
                    {
                        loginVM.IsFirstTimeLogin = true;
                    }
                    if (!string.IsNullOrWhiteSpace(loginVM.FirstName))
                        loginVM.Initial = StringFilter.GetInitialChar(loginVM.FirstName);

                    if ((!string.IsNullOrWhiteSpace(loginVM.FirstName)) && (!string.IsNullOrWhiteSpace(loginVM.LastName)))
                        loginVM.InitialChars = StringFilter.GetInitials(loginVM.FirstName);

                    loginVM.ParentId = user.ParentId;

                    CommonData _CommonData = new CommonData(_httpContextAccessor);

                    if (loginVM.LanguageId > 0)
                    {
                        loginVM.ProfileLanguage = _CommonData.LanguageNameFromId(loginVM.LanguageId.ToString());
                        loginVM.SelectedLanguage = _CommonData.LanguageNameFromId(loginVM.LanguageId.ToString());
                    }
                  
                    _session.SetString(KeyEnums.SessionKeys.UserId.ToString(), loginVM.Id.ToString());
                    _session.SetString(KeyEnums.SessionKeys.FirstName.ToString(), loginVM.FirstName.ToString());
                    _session.SetString(KeyEnums.SessionKeys.UserRole.ToString(), loginVM.RoleId.ToString());                    

                    if (!string.IsNullOrWhiteSpace(loginVM.UserName))
                    {
                        _session.SetString(KeyEnums.SessionKeys.UserEmailID.ToString(), loginVM.UserName.ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(loginVM.Profile_Logo))
                    {
                        _session.SetString(KeyEnums.SessionKeys.UserLogo.ToString(), loginVM.Profile_Logo.ToString());
                    }
                    
                    _session.SetString(KeyEnums.SessionKeys.UserSession.ToString(), JsonConvert.SerializeObject(loginVM));
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "SetSession(User user)", ex.Source, ex.Message, ex);
            }
        }

        public LoginVM UpdateSession(LoginVM _LoginVM)
        {
            LoginVM loginVM = new LoginVM();
            try
            {
                _session.SetString(KeyEnums.SessionKeys.UserSession.ToString(), JsonConvert.SerializeObject(loginVM));
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "UpdateSession()", ex.Source, ex.Message, ex);
            }
            return loginVM;
        }

        public void SetUserLanguage(long LanguageId)
        {
            _session.SetString(KeyEnums.SessionKeys.LanguageId.ToString(), LanguageId.ToString());
        }

        public void SetAssessmentData(Int64 ClassID, string TermID, Int64 UserID)
        {
            _session.SetString("AssessmentClassID", ClassID.ToString());
            _session.SetString("AssessmentTermID", TermID.ToString());
            _session.SetString("AssessmentUserID", UserID.ToString());
        }

     

        public void SetPortfolioData(Int64 ClassID, string TermID, Int64 UserID)
        {
            _session.SetString("PortfolioClassID", ClassID.ToString());
            _session.SetString("PortfolioTermID", TermID.ToString());
            _session.SetString("PortfolioUserID", UserID.ToString());
        }

        public void SetPlayLearnData(Int64 ClassID)
        {
            _session.SetString("PlayLearnClassID", ClassID.ToString());
        }

        public void SetSentenceData(Int64 ClassID, int DomainID)
        {
            _session.SetString("SentenceClassID", ClassID.ToString());
            _session.SetString("SentenceDomainID", DomainID.ToString());
        }

        public void SetWorksheetData(Int64 ClassID)
        {
            _session.SetString("WorksheetClassID", ClassID.ToString());
        }

        

        public bool IsValidUser(Int64 UserID,string userRole,string RoleIDs)
        {
            bool isAllowed = false;
            try
            {
                if (_session.GetString(KeyEnums.SessionKeys.UserRole.ToString()) == null || UserID == 0)
                    isAllowed = false;
                else if (RoleIDs.ToLower().Contains(_session.GetString(KeyEnums.SessionKeys.UserRole.ToString()).ToString().ToLower()))
                    isAllowed = true;

                //Log.WriteInfoLog(_module, "IsValidUser(UserID:" + UserID + ",RoleIDs:" + RoleIDs + ")", _session.GetString(KeyEnums.SessionKeys.UserRole.ToString()).ToString().ToLower() + ":" + RoleIDs.ToLower().Contains(_session.GetString(KeyEnums.SessionKeys.UserRole.ToString()).ToString().ToLower()), isAllowed.ToString());
               

                
             
                if (!isAllowed)
                {
                    string[] userRoles = RoleIDs.Split(',');
                    if (userRoles.Contains("1")  && userRole == "3" || userRoles.Contains("2") && userRole == "3")
                    {
                        _httpContextAccessor.HttpContext.Response.Redirect("/Home/Index", true);
                    }
                    else if(userRoles.Contains("3") && userRole == "1" || userRole == "2")
                    {
                        _httpContextAccessor.HttpContext.Response.Redirect("/Admin/Index", true);
                    }
                    else
                    {
                        _httpContextAccessor.HttpContext.Response.Redirect("/Users/Login", true);
                    } 
                                        
                    
                        
                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsValidUser(UserID:" + UserID + ",RoleIDs:" + RoleIDs + ")", ex.Source, ex.Message, ex);
            }
            return isAllowed;
        }
    }
}