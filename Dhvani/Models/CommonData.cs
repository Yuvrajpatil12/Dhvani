using Core.Business.BusinessFacade;
using Core.Entity;
using Core.Entity.Enums;
using Core.Utility.Common;

//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;

//using SelectPdf;

namespace Core.Models
{
    public class CommonData
    {
        private readonly string _module = "Core.Models.CommonData";
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Int64 _userId = 0;

        public CommonData(IHttpContextAccessor httpContextAccessor = null)
        {
            _userId = httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString()) != null ? Convert.ToInt64(httpContextAccessor.HttpContext.Session.GetString(KeyEnums.SessionKeys.UserId.ToString())) : 0;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<SelectListItem> LanguageList(long SelectedLang = 0)
        {
            List<SelectListItem> _LanguageList = new List<SelectListItem>();
            LanguageMasterBusinessFacade _LanguageMasterBusinessFacade = new LanguageMasterBusinessFacade();
            try
            {
                List<LanguageMaster> _List = _LanguageMasterBusinessFacade.GetAllRecordsList();
                if (_List.Count > 0)
                {
                    for (int i = 0; i < _List.Count; i++)
                    {
                        SelectListItem _SelectListItem = new SelectListItem();
                        _SelectListItem.Text = _List[i].Language;
                        _SelectListItem.Value = Convert.ToString(_List[i].ID);
                        if (_List[i].ID == SelectedLang && SelectedLang > 0)
                        {
                            _SelectListItem.Selected = true;
                        }
                        _LanguageList.Add(_SelectListItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "LanguageList()", ex.Source, ex.Message, ex);
            }
            return _LanguageList;
        }

        public string LanguageNameFromId(string SelectedLang)
        {
            string LanguageName = "";
            try
            {
                List<SelectListItem> ListSelectListItem = LanguageList();

                if (ListSelectListItem.FindAll(X => X.Value == SelectedLang).Count > 0)
                {
                    LanguageName = ListSelectListItem.FindAll(X => X.Value == SelectedLang)[0].Text;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "LanguageNameFromId(SelectedLang:" + SelectedLang + ")", ex.Source, ex.Message, ex);
            }

            return LanguageName.ToLower();
            // return "en";
        }

        public List<SelectListItem> GetPageSizes()
        {
            List<SelectListItem> pageSizes = new List<SelectListItem>();
            try
            {
                pageSizes = new List<SelectListItem>
                {
                    //new SelectListItem { Text = "5", Value = "5"} ,
                    new SelectListItem { Text = "10", Value = "10"} ,
                    new SelectListItem { Text = "25", Value = "25"} ,
                    new SelectListItem { Text = "50", Value = "50" },
                    new SelectListItem { Text = "100", Value = "100" }
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetPageSizes()", ex.Source, ex.Message, ex);
            }
            return pageSizes;
        }

        public List<SelectListItem> GetOtherRelationships()
        {
            List<SelectListItem> relationships = new List<SelectListItem>();
            try
            {
                relationships = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Select Relationship", Value = "0"} ,
                    new SelectListItem { Text = "pickup", Value = "Pickup" },
                    new SelectListItem { Text = "emergency", Value = "Emergency" }
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetRelationships()", ex.Source, ex.Message, ex);
            }
            return relationships;
        }

        public List<SelectListItem> GetRelationships()
        {
            List<SelectListItem> relationships = new List<SelectListItem>();
            try
            {
                relationships = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Mother", Value = "mother"},
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetRelationships()", ex.Source, ex.Message, ex);
            }
            return relationships;
        }

        public List<SelectListItem> GetFather()
        {
            List<SelectListItem> relationships = new List<SelectListItem>();
            try
            {
                relationships = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Father", Value = "father"},
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetFather()", ex.Source, ex.Message, ex);
            }
            return relationships;
        }

        //public string GeneratePDF(string HTML, string FileNameWithoutExtention, string UserId, string PDFType, string hostPath, string FolderName)
        //{
        //    string stringWriter = HTML;
        //    HtmlToPdf converter = new HtmlToPdf();

        //    converter.Options.MarginBottom = 0;
        //    converter.Options.MarginTop = 0;

        //    string BasePath = null;
        //    hostPath = hostPath + "//wwwroot";

        //    if (PDFType == "Invoice")
        //    {
        //        BasePath = Path.Combine(hostPath) + ConstantsCommon.InvoiceReportsPath + "\\" + FolderName + "\\";
        //    }

        //    if (PDFType == "Receipt")
        //    {
        //        BasePath = Path.Combine(hostPath) + ConstantsCommon.PaymentReceiptPath + "\\" + FolderName + "\\";
        //    }
        //    if (PDFType == "Deposit")
        //    {
        //        BasePath = Path.Combine(hostPath) + ConstantsCommon.DepositPath + "\\" + FolderName + "\\";
        //    }

        //    string PDF_Path = BasePath + FileNameWithoutExtention + ".pdf";

        //    bool exists = System.IO.Directory.Exists(BasePath);

        //    if (!exists)
        //        System.IO.Directory.CreateDirectory(BasePath);

        //    if (!System.IO.File.Exists(PDF_Path))
        //    {
        //        converter.Options.ScaleImages = true;
        //        converter.Options.LazyImagesLoadingEnabled = true;
        //        PdfDocument doc = converter.ConvertHtmlString(stringWriter);

        //        doc.Save(PDF_Path);
        //    }

        //    Console.WriteLine("pdf path : " + PDF_Path);

        //    return PDF_Path;
        //}
    }
}