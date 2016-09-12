using System.Web;
using System.Web.Mvc;

namespace ReaValley.DataMapper
{
    public class CountryViewEngine : RazorViewEngine
    {
        public CountryViewEngine() : base()
        {
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.%1cshtml",
                "~/Areas/{2}/Views/{1}/{0}.%1vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.%1cshtml",
                "~/Areas/{2}/Views/Shared/{0}.%1vbhtml"
            };

            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.%1cshtml",
                "~/Areas/{2}/Views/{1}/{0}.%1vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.%1cshtml",
                "~/Areas/{2}/Views/Shared/{0}.%1vbhtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.%1cshtml",
                "~/Areas/{2}/Views/{1}/{0}.%1vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.%1cshtml",
                "~/Areas/{2}/Views/Shared/{0}.%1vbhtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.%1cshtml",
                "~/Views/{1}/{0}.%1vbhtml",
                "~/Views/Shared/{0}.%1cshtml",
                "~/Views/Shared/{0}.%1vbhtml"
            };

            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.%1cshtml",
                "~/Views/{1}/{0}.%1vbhtml",
                "~/Views/Shared/{0}.%1cshtml",
                "~/Views/Shared/{0}.%1vbhtml"
            };

            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.%1cshtml",
                "~/Views/{1}/{0}.%1vbhtml",
                "~/Views/Shared/{0}.%1cshtml",
                "~/Views/Shared/{0}.%1vbhtml"
            };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var country = HttpContext.Current.Request.QueryString["country"];
            var nameSpace = string.IsNullOrEmpty(country) ? string.Empty : country + ".";
            return base.CreatePartialView(controllerContext, partialPath.Replace("%1", nameSpace));
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var country = HttpContext.Current.Request.QueryString["country"];
            var nameSpace = string.IsNullOrEmpty(country) ? string.Empty : country + ".";
            return base.CreateView(controllerContext, viewPath.Replace("%1", nameSpace), masterPath.Replace("%1", nameSpace));
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            var country = HttpContext.Current.Request.QueryString["country"];
            var nameSpace = string.IsNullOrEmpty(country) ? string.Empty : country + ".";
            return base.FileExists(controllerContext, virtualPath.Replace("%1", nameSpace));
        }
    }
}