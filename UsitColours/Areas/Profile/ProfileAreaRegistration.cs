using System.Web.Mvc;

namespace UsitColours.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "Profile_default",
                "Profile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}