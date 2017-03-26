using Antlr.Runtime.Misc;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace UsitColours.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            this.GetLoggedUserId = () => User.Identity.GetUserId();
        }

        public virtual Func<string> GetLoggedUserId { get; set; }
    }
}