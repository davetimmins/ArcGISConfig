using ServiceStack.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArcGISConfiguration.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(ILogFactory logger)
            : base(logger)
        {
        }

        //
        // GET: /Home/
        public ActionResult Index(string role)
        {
            TempData["Role"] = String.IsNullOrWhiteSpace(role) ? "basemap" : role;
            return View();
        }
    }
}
