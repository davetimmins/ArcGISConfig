using ArcGISConfiguration.API.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;
using ServiceStack.Common;

namespace ArcGISConfiguration.API.ServiceInterface
{
    [Restrict(VisibleInternalOnly = true)]
    public class MapDataService : ServiceStack.ServiceInterface.Service
    {
        public object Get(MapData request)
        {
            if (string.IsNullOrWhiteSpace(request.Role)) throw new ArgumentNullException("Role");
            
            var cacheKey = UrnId.Create<MapData>(request.Role);
            return base.RequestContext.ToOptimizedResultUsingCache(this.Cache, cacheKey, () =>
            {
                var result = Db.SelectParam<MapData>(q => q.Role == request.Role).FirstOrDefault();
                if (result == null) 
                    throw ServiceStack.Common.Web.HttpError.NotFound(String.Format("No data exists for role '{0}'", request.Role));

                return result;
            });
        }
    }
}
