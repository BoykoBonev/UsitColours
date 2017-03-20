using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace UsitColours.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetLoggedUserId
        {
            get
            {
                return this.User?.Identity.GetUserId();
            }
        }
    }
}