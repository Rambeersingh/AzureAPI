using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Omya.AzureApi
{
    public static class Constants
    {
        #region Constant variables

        public static string _siteurl = "SiteUrl";
        public static string _applicationid = "ApplicationID";
        public static string _domainurl = "DomainUrl";
        public static string _certificatepath = "CertificatePath";
        public static string _passsword = "Passsword";
        public static string _baseurl = "BaseUrl";
        public static string _omyaapps = "OmyaApps";
        public static string _omyaapplanguage = "OmyaAppLanguage";
        public static string _omyaappinfos = "OmyaAppInfos";
        public static string _plants = "Plants";

        #endregion

        #region Web config values

        public static string SiteUrl;
        public static string ApplicationID;
        public static string DomainUrl;
        public static string CertificatePath;
        public static string Passsword;
        public static string BaseUrl;
        public static string OmyaApps;
        public static string OmyaAppLanguage;
        public static string OmyaAppInfos;
        public static string Plants;

        #endregion

        #region Static constructor

        static Constants()
        {
            SiteUrl = WebConfigurationManager.AppSettings[_siteurl];
            ApplicationID = WebConfigurationManager.AppSettings[_applicationid];
            DomainUrl = WebConfigurationManager.AppSettings[_domainurl];
            CertificatePath = WebConfigurationManager.AppSettings[_certificatepath];
            Passsword = WebConfigurationManager.AppSettings[_passsword];
            BaseUrl = WebConfigurationManager.AppSettings[_baseurl];
            OmyaApps = WebConfigurationManager.AppSettings[_omyaapps];
            OmyaAppLanguage = WebConfigurationManager.AppSettings[_omyaapplanguage];
            OmyaAppInfos = WebConfigurationManager.AppSettings[_omyaappinfos];
            Plants = WebConfigurationManager.AppSettings[_plants];
        }

        #endregion

        #region CAML query strings

        internal static string CAML_OmyaApps = @"<View><Query><Where><And><And><And><And><Eq>" +
                        "<FieldRef Name='Title' /><Value Type='Text'>{0}</Value></Eq><Eq>" +
                        "<FieldRef Name='Key' /><Value Type='Text'>{1}</Value></Eq></And><Eq>" +
                        "<FieldRef Name='AppType' /><Value Type='Text'>{2}</Value></Eq></And><Eq>" +
                        "<FieldRef Name='UniqueAppGUID' /><Value Type='Text'>{3}</Value></Eq></And><Eq>" +
                        "<FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></And></Where></Query><RowLimit>1</RowLimit></View>";

        internal static string CAML_OmyaAppLanguages = @"<View><Query><Where><And><Eq>" +
                        "<FieldRef Name='OmyaAppID' LookupValue='TRUE' /><Value Type='Lookup'>{0}</Value></Eq><Eq>" +
                        "<FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></And></Where></Query></View>";

        internal static string CAML_OmyaInfoItems = @"<View><Query><Where><And><Eq>" +
            "<FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq><And><Eq>" +
            "<FieldRef Name='OmyaAppID' LookupValue='TRUE' /><Value Type='Lookup'>{0}</Value></Eq><Eq>" +
            "<FieldRef Name='Language' /><Value Type='Text'>{1}</Value></Eq></And></And></Where></Query></View>";

        internal static string CAML_OmyaPlants = @"<View><Query><Where><And><Eq>" +
            "<FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq><And><Eq>" +
            "<FieldRef Name='OmyaAppID' LookupValue='TRUE' /><Value Type='Lookup'>{0}</Value></Eq><Eq>" +
            "<FieldRef Name='Language' /><Value Type='Text'>{1}</Value></Eq></And></And></Where></Query></View>";

        internal static string CAML_OmyaPlantAttachments = @"<View><Query><Where><And><And><Eq>" +
            "<FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq>" +
            "<Eq><FieldRef Name='ID' /><Value Type='Integer'>{2}</Value></Eq></And>" +
            "<And><Eq><FieldRef Name='OmyaAppID' LookupValue='TRUE' /><Value Type='Lookup'>{0}</Value></Eq>" +
            "<Eq><FieldRef Name='Language' /><Value Type='Text'>{1}</Value></Eq></And></And></Where></Query></View>";

        #endregion

        #region Error messages

        public static string AppNotFound = "Oops! Omya app not found.";
        public static string DuplicateAppFound = "Oops! Multiple Omya apps were found.";

        #endregion
    }
}