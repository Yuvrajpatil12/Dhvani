using Azure.Storage.Blobs;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Models;
using Core.Resources;
using Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Dhvani.Models;
using Core.Business.BusinessFacade;
using Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Core.Utility;


namespace Dhvani.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class SSMLController : Controller
	{
		private readonly string _module = "Core.Controllers.App.SSMLController";
		private JsonMessage _jsonMessage = null;
		private IHostingEnvironment _hostingEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;
		private IMemoryCache _cache;
		private Helper _helper;
		private string RootPath = ConstantsCommon.RootPath;
		private string SSMLPath = ConstantsCommon.SSMLPath;
		private string CoreDomain = ConstantsCommon.CoreDomain;

		public SSMLController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
		{
			_hostingEnvironment = environment;
			_httpContextAccessor = httpContextAccessor;
			_cache = cache;
			_helper = new Helper(_httpContextAccessor);
		}

		[Authorize]
		[HttpPost]
		public IActionResult saveDictionary(SSMLViewModel objVM)
		{
			var requestData = JsonConvert.SerializeObject(new { objVM });

			try
			{
				Log.WriteInfoLogWithoutMail(_module, "saveDictionary()", "INFOLOG ", requestData);
			}
			catch (Exception ex)
			{
			}

			try
			{
				string strUserID = "";
				if (objVM.UserAPIKey == "cms" || objVM.UserAPIKey == "cmsssml")
				{
					//get the user details from the session
					strUserID = _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? _httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) : "";
					if (string.IsNullOrWhiteSpace(strUserID))
					{
						_jsonMessage = new JsonMessage(false, Resource.lbl_error, "Invalid User", KeyEnums.JsonMessageType.ERROR, "", "1");
						return Json(_jsonMessage);
					}
				}
				else
				{
					//the request is from api call
					//get the user details from the api key
					strUserID = "0";

					UsersBusinessFacade objBF = new UsersBusinessFacade();
					Users objEntity = new Users();
					objEntity = objBF.GetUserIdFromAPIKey(objVM.UserAPIKey);

					if (objEntity.ID > 0)
					{
						string strErrorCode = "";
						string strErrorMessage = "Invalid Key";
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
						else
						{
							strUserID = Convert.ToString(objEntity.ID);
						}
						if (strErrorCode == "1")
						{
							_jsonMessage = new JsonMessage(false, Resource.lbl_error, strErrorMessage, KeyEnums.JsonMessageType.ERROR, "", "", requestData);
							return Json(_jsonMessage);
						}
					}
				}

				bool IsSuccess = false;
				string voiceRegion = !string.IsNullOrWhiteSpace(objVM.Locale) ? Convert.ToString(objVM.Locale.Trim()) : "";
				var UserText = !string.IsNullOrWhiteSpace(objVM.UserText) ? Convert.ToString(objVM.UserText.Trim()) : "";
				var UserAlternateText = !string.IsNullOrWhiteSpace(objVM.UserAlternateText) ? Convert.ToString(objVM.UserAlternateText.Trim()) : "";

				var XmlDictionaryPathAzure = ConstantsCommon.SSMLPath + "/" + "region.xml";

				if (UserText != "" && UserAlternateText != "")
				{
					XDocument xDocument = XDocument.Load(XmlDictionaryPathAzure);

					string Xmlstr = xDocument.ToString();
					Xmlstr = XMLFilter.RemoveAllNamespaces(Xmlstr);

					XmlDocument File_obj = new XmlDocument();
					File_obj.LoadXml(Xmlstr);
					
					XDocument xmlDocFrom = XDocument.Parse(File_obj.OuterXml);
					XElement root = xmlDocFrom.Element("lexicon");
					IEnumerable<XElement> rows = root.Descendants("lexeme");
					XElement firstRow = rows.First();

					firstRow.AddBeforeSelf(
					   new XElement("lexeme",
					   new XElement("grapheme", UserText),
					   new XElement("alias", UserAlternateText)));
					string Xmlstr1 = xmlDocFrom.ToString();

					var lexicon = "<lexicon version=\"1.0\"  xmlns=\"http://www.w3.org/2005/01/pronunciation-lexicon\"  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.w3.org/2005/01/pronunciation-lexicon    http://www.w3.org/TR/2007/CR-pronunciation-lexicon-20071212/pls.xsd\"  alphabet=\"ipa\" xml:lang=\"" + voiceRegion + "\">";
					Xmlstr1 = Xmlstr1.Replace("<lexicon>", lexicon);
					
					XmlDocument File_obj1 = new XmlDocument();
					File_obj1.LoadXml(Xmlstr1);

					XDocument xmlDocFrom1 = XDocument.Parse(File_obj1.OuterXml);
					var XmlDictionaryPath = SSMLPath + @"\" + strUserID + @"\" + voiceRegion + "_region.xml";

                    //var XmlDictionaryBlobPath = strUserID + @"\" + voiceRegion + "_region.xml";

                    if (!Directory.Exists(SSMLPath + @"\" + strUserID))
						Directory.CreateDirectory(SSMLPath + @"\" + strUserID);

					xmlDocFrom1.Save(XmlDictionaryPath);

					BlobServiceClient blobServiceClient = new BlobServiceClient(ConstantsCommon.BlobStorageConnectionString);
					BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ConstantsCommon.BlobContainerName);
					BlobClient blobClient = containerClient.GetBlobClient(XmlDictionaryPath);

					blobClient.Upload(XmlDictionaryPath, overwrite: true);
					IsSuccess = true;
				}

				if (IsSuccess)
					_jsonMessage = new JsonMessage(true, Resource.lbl_Cap_success, Resource.lbl_msg_dataSavedSuccessfully, KeyEnums.JsonMessageType.SUCCESS, "", "");
				else
					_jsonMessage = new JsonMessage(false, Resource.lbl_error, "Something went wrong! Please try after some time.", KeyEnums.JsonMessageType.ERROR, "", "");
			}
			catch (Exception ex)
			{
				_jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_msg_internalServerErrorOccurred, KeyEnums.JsonMessageType.DANGER, "", Resource.lbl_exception, ex.Message);
				Log.WriteLog(_module, "saveDictionary(" + requestData + ")", ex.Source, ex.Message);

			}
			return Json(_jsonMessage);
		}
	}
}