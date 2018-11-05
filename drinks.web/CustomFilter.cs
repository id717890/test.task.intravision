namespace drinks.web
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using infrastructure;

    public class CustomFilter : ActionFilterAttribute
    {
        private static readonly string _secret = ConfigurationManager.AppSettings["secret"];

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RouteData.Values.ContainsKey("secret"))
            {
                var secretCookie = HttpContext.Current.Request.Cookies["secret"];
                if (secretCookie?.Value == null)
                {
                    filterContext.Result = new RedirectResult("/");
                } else
                {
                    if (!GuardService.CheckSecret(secretCookie.Value)) filterContext.Result = new RedirectResult("/");
                }
            }
            else
            {
                var secret = filterContext.RouteData.Values["secret"].ToString();
                if (!GuardService.CheckSecret(secret))
                {
                    filterContext.Result = new RedirectResult("/");
                } else
                {
                    HttpContext.Current.Response.SetCookie(new HttpCookie("secret")
                    {
                        Value = secret,
                        Expires = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["secret_expiration"]))
                    });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}