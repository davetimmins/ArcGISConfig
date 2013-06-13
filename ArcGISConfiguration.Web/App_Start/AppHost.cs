using System.Web.Mvc;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;
using ArcGISConfiguration.API.ServiceModel;
using ArcGISConfiguration.API.ServiceInterface;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.Logging.NLogger;
using ServiceStack.Api.Swagger;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ArcGISConfiguration.Web.App_Start.AppHost), "Start")]

//IMPORTANT: Add the line below to MvcApplication.RegisterRoutes(RouteCollection) in the Global.asax:
//routes.IgnoreRoute("api/{*pathInfo}"); 

/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace ArcGISConfiguration.Web.App_Start
{
	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("ArcGIS map configuration data service", typeof(MapDataService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
            ServiceStack.Text.JsConfig.EscapeUnicode = false;
            ServiceStack.Text.JsConfig.TryToParsePrimitiveTypeValues = true;

            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<ServiceStack.Logging.ILogFactory>(new NLogFactory());
		
			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));

            container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", false, ServiceStack.OrmLite.Sqlite.SqliteOrmLiteDialectProvider.Instance)
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            });
            var dbFactory = container.Resolve<IDbConnectionFactory>();

            if (!dbFactory.Run(db => db.TableExists("MapData")))
            {
                dbFactory.Run(db => db.CreateTableIfNotExists<MapData>());
                dbFactory.Run(db => db.InsertAll(SeedData.MapData()));
            }

            Plugins.Add(new SwaggerFeature());
		}
        
		public static void Start()
		{
			new AppHost().Init();
		}
	}
}