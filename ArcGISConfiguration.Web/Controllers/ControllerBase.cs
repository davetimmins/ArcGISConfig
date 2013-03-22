using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;

namespace ArcGISConfiguration.Web.Controllers
{
    [ProfilingActionFilter]
    public abstract class ControllerBase : ServiceStackController
    {
        readonly protected ILog Log;

        protected ControllerBase(ILogFactory logger)
        {
            Log = logger.GetLogger(GetType());
        }
    }
}