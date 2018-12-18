using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeDevPnP.Core;
using Microsoft.SharePoint.Client;
using Omya.AzureApi.Models;

namespace Omya.AzureApi.Models
{
    public static class OmyaRepository
    {
        internal static ClientContext GetClientContext()
        {
            ClientContext _context = new OfficeDevPnP.Core.AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(Constants.SiteUrl,
                Constants.ApplicationID,
                Constants.DomainUrl,
                Constants.CertificatePath,
                Constants.Passsword);

            return _context;
        }

        internal static Site LoadSite(ClientContext _context)
        {
            //Get the Site Collection
            Site _site = _context.Site;
            _context.Load(_site);
            _context.ExecuteQuery();
            return _site;
        }

        internal static Web LoadWeb(ClientContext _context)
        {
            // Get the Web
            Web _web = _context.Web;
            _context.Load(_web);
            _context.ExecuteQuery();
            return _web;
        }

        internal static OmyaApp GetAppObject(ClientContext _context, AppParam _appParam)
        {
            OmyaApp _omyaapp = new OmyaApp();

            List _OmyaApps = _context.Web.Lists.GetByTitle(Constants.OmyaApps);

            CamlQuery camlQueryOmyaApps = new CamlQuery();
            camlQueryOmyaApps.ViewXml = String.Format(Constants.CAML_OmyaApps,
                _appParam.AppTitle,
                _appParam.AppKey,
                _appParam.AppType,
                _appParam.AppGuid);

            ListItemCollection collOmyaAppsItem = _OmyaApps.GetItems(camlQueryOmyaApps);
            _context.Load(collOmyaAppsItem);


            List _OmyaAppLanguage = _context.Web.Lists.GetByTitle(Constants.OmyaAppLanguage);

            CamlQuery camlQueryOmyaAppLanguage = new CamlQuery();

            camlQueryOmyaAppLanguage.ViewXml = String.Format(Constants.CAML_OmyaAppLanguages,
                _appParam.AppGuid);

            ListItemCollection collOmyaAppLanguageItems = _OmyaAppLanguage.GetItems(camlQueryOmyaAppLanguage);
            _context.Load(collOmyaAppLanguageItems);

            _context.ExecuteQuery();

            if (collOmyaAppsItem.Count <= 0)
                throw (new Exception(Constants.AppNotFound));

            if (collOmyaAppsItem.Count > 1)
                throw (new Exception(Constants.DuplicateAppFound));

            _omyaapp.ID = collOmyaAppsItem[0].Id;
            _omyaapp.Title = (string)collOmyaAppsItem[0]["Title"];
            _omyaapp.Key = (string)collOmyaAppsItem[0]["Key"];
            _omyaapp.AppType = (string)collOmyaAppsItem[0]["AppType"];
            _omyaapp.UniqueAppGuid = (string)collOmyaAppsItem[0]["UniqueAppGUID"];

            string _defaultlanguage = String.Empty;
            string _langugage = String.Empty;
            foreach (ListItem item in collOmyaAppLanguageItems)
            {
                Boolean _default = false;
                string _lang = String.Empty;
                _lang = (string)item["Language"];
                _default = (Boolean)item["DefaultLang"];

                if (_lang == _appParam.AppLanguage)
                {
                    _langugage = _lang;
                    break;
                }

                if (_default)
                    _defaultlanguage = _lang;

            }

            if (!String.IsNullOrEmpty(_langugage))
                _omyaapp.Langugage = _langugage;
            else
                _omyaapp.Langugage = _defaultlanguage;

            return _omyaapp;
        }

        internal static List<AppInfos> GetMenuItems(ClientContext _context, OmyaApp _omyaapp)
        {
            List<AppInfos> _appinfos = new List<AppInfos>();

            List _OmyaInfo = _context.Web.Lists.GetByTitle(Constants.OmyaAppInfos);

            CamlQuery camlQueryOmyaAppInfos = new CamlQuery();

            camlQueryOmyaAppInfos.ViewXml = String.Format(Constants.CAML_OmyaInfoItems,
                _omyaapp.UniqueAppGuid, _omyaapp.Langugage);

            ListItemCollection collOmyaAppInfosItems = _OmyaInfo.GetItems(camlQueryOmyaAppInfos);
            //_context.Load(collOmyaAppInfosItems);
            _context.Load(collOmyaAppInfosItems, i => i.IncludeWithDefaultProperties(item => item.AttachmentFiles));
            
            _context.ExecuteQuery();

            foreach (ListItem item in collOmyaAppInfosItems)
            {
                _appinfos.Add(new AppInfos()
                {
                    ID = item.Id,
                    Title = (string)item["Title"],
                    Key = (string)item["Key"],
                    SegmentID = (CheckIfNotNull(item["SegmentID"]) ? Convert.ToInt32((Double)item["SegmentID"]) : (int?)null),
                    SegmentOrder = (CheckIfNotNull(item["SegmentOrder"]) ? Convert.ToInt32((Double)item["SegmentOrder"]) : (int?)null),
                    ItemType = (string)item["ItemType"],
                    Tags = (string)item["Tags"],
                    Platform = (string[])item["Platform"],
                    SegmentLink = (string)item["SegmentLink"],
                    Language = (string)item["Language"],
                    HasAttachments = ((item.AttachmentFiles.Count > 0) ? true : false),
                    Attachments = GetItemAttachments(item),
                    Created = (DateTime)item["Created"],
                    Author = ((FieldUserValue)item["Author"]).LookupValue,
                    Modified = (DateTime)item["Modified"],
                    Editor = ((FieldUserValue)item["Editor"]).LookupValue,
                });
            }

            return _appinfos;
        }

        internal static List<OmyaPlants> GetPlants(ClientContext _context, OmyaApp _omyaapp)
        {
            List<OmyaPlants> _omyaplants = new List<OmyaPlants>();

            List _OmyaPlants = _context.Web.Lists.GetByTitle(Constants.Plants);

            CamlQuery camlQueryOmyaPlants = new CamlQuery();

            camlQueryOmyaPlants.ViewXml = String.Format(Constants.CAML_OmyaPlants,
                _omyaapp.UniqueAppGuid, _omyaapp.Langugage);

            ListItemCollection collOmyaAppPlantsItems = _OmyaPlants.GetItems(camlQueryOmyaPlants);
            _context.Load(collOmyaAppPlantsItems, i => i.IncludeWithDefaultProperties(item => item.AttachmentFiles));

            _context.ExecuteQuery();

            foreach (ListItem item in collOmyaAppPlantsItems)
            {
                _omyaplants.Add(new OmyaPlants()
                {
                    ID = item.Id,
                    SiteCode = (string)item["Title"],
                    Region = (string)item["Region"],
                    Country = (string)item["Country"],
                    PlantName = (string)item["PlantName"],
                    Mineral = (string)item["Mineral"],
                    Latitude = (string)item["Latitude"],
                    Longitude = (string)item["Longitude"],
                    Certifications = (string[])item["Certifications"],
                    Language = (string)item["Language"],
                    HasAttachments = ((item.AttachmentFiles.Count > 0) ? true : false),
                    Attachments = GetItemAttachments(item),
                    Created = (DateTime)item["Created"],
                    Author = ((FieldUserValue)item["Author"]).LookupValue,
                    Modified = (DateTime)item["Modified"],
                    Editor = ((FieldUserValue)item["Editor"]).LookupValue,
                });
            }

            return _omyaplants;
        }

        internal static List<OmyaAttachment> GetMenuAttachment(ClientContext _context, Site _site, OmyaApp _omyaapp, SegmentParam _segmentparam)
        {
            List<OmyaAttachment> _segmentAttachment = null;

            List _OmyaInfo = _context.Web.Lists.GetByTitle(Constants.OmyaAppInfos);

            CamlQuery camlQueryOmyaAppInfos = new CamlQuery();

            camlQueryOmyaAppInfos.ViewXml = String.Format(Constants.CAML_OmyaPlantAttachments,
                _omyaapp.UniqueAppGuid, _omyaapp.Langugage, _segmentparam.ID.ToString());

            ListItemCollection collOmyaAppInfosItems = _OmyaInfo.GetItems(camlQueryOmyaAppInfos);
            _context.Load(collOmyaAppInfosItems);
            
            _context.ExecuteQuery();

            foreach (ListItem item in collOmyaAppInfosItems)
            {
                string uri = _site.Url + "/Lists/" + Constants.OmyaAppInfos + "/Attachments/" + item["ID"].ToString();
                _segmentAttachment = GetAttachment(_context, uri);
            }

            return _segmentAttachment;
        }

        internal static List<OmyaAttachment> GetPlantAttachment(ClientContext _context, Site _site, OmyaApp _omyaapp, PlantParam _plantparam)
        {
            List<OmyaAttachment> _plantAttachment = null;

            List _plants = _context.Web.Lists.GetByTitle(Constants.Plants);

            CamlQuery camlQueryOmyaPlants = new CamlQuery();

            camlQueryOmyaPlants.ViewXml = String.Format(Constants.CAML_OmyaPlantAttachments,
                _omyaapp.UniqueAppGuid, _omyaapp.Langugage, _plantparam.ID.ToString());

            ListItemCollection collOmyaAppInfosItems = _plants.GetItems(camlQueryOmyaPlants);
            _context.Load(collOmyaAppInfosItems);

            _context.ExecuteQuery();

            foreach (ListItem item in collOmyaAppInfosItems)
            {
                string uri = _site.Url + "/Lists/" + Constants.Plants + "/Attachments/" + item["ID"].ToString();
                _plantAttachment = GetAttachment(_context, uri);
            }

            return _plantAttachment;
        }

        internal static bool CheckIfNotNull(object _obj)
        {
            bool flag = true;

            if (_obj == null)
                flag = false;

            return flag;
        }

        internal static string[] GetItemAttachments(ListItem _item)
        {
            List<string> _attachments = new List<string>();

            foreach(Attachment _attach in _item.AttachmentFiles)
            {
                _attachments.Add(_attach.FileName);
            }

            return _attachments.ToArray();
        }

        internal static List<OmyaAttachment> GetAttachment(ClientContext context, string uri)
        {
            List<OmyaAttachment> _attachments = new List<OmyaAttachment>();
            try
            {
                var files = context.Web.GetFolderByServerRelativeUrl(uri).Files;
                context.Load(files);
                context.ExecuteQuery();
                foreach (Microsoft.SharePoint.Client.File file in files)
                {
                    ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                    context.Load(file);
                    context.ExecuteQuery();
                    using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                    {
                        if (data != null)
                        {
                            data.Value.CopyTo(mStream);
                            byte[] imageArray = mStream.ToArray();
                            string b64String = Convert.ToBase64String(imageArray);

                            _attachments.Add(new OmyaAttachment()
                            {
                                UniqueID = file.UniqueId,
                                Name = file.Name,
                                Length = file.Length,
                                File = imageArray,
                                Created = file.TimeCreated,
                                Modified = file.TimeLastModified
                            });
                        }
                    }
                }
            }
            catch (Exception exp)
            {

            }
            return _attachments;
        }

    }
}
