using ArcGISConfiguration.API.ServiceInterface;
using ArcGISConfiguration.API.ServiceModel;
using ServiceStack.OrmLite;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArcGISConfiguration.Test
{
    public sealed class MapDataServiceTests : IDisposable
    {
        const string BaseUri = "http://localhost:1337/";
        AppHost _appHost;

        public MapDataServiceTests()
        {
            _appHost = new AppHost();
            _appHost.Init();
            _appHost.Start(BaseUri);
        }

        [Fact]
        public void MapDataExists()
        {
            var client = new JsonServiceClient(BaseUri);

            var data = client.Get<List<MapData>>("/mapdata/basemap");

            Assert.True(data.Any());
        }

        [Fact]
        public void MapDataIsFilteredByType()
        {
            var client = new JsonServiceClient(BaseUri);

            var expected = SeedData.MapData().Last();
            var data = client.Get<List<MapData>>("/mapdata/" + expected.Role);

            Assert.False(SeedData.MapData().Count == data.Count);
            Assert.Equal(1, data.Count);
            Assert.Equal(expected.Role, data.First().Role);
            Assert.ReferenceEquals(data.First(), expected);
        }

        [Fact]
        public void MapDataThrowsExceptionForEmptyRole()
        {
            var client = new JsonServiceClient(BaseUri);

            Exception ex = Assert.Throws<ServiceStack.ServiceClient.Web.WebServiceException>(() => client.Get<List<MapData>>("/mapdata/ /"));
        }

        [Fact]
        public void MapDataThrowsExceptionForUnmatchedRole()
        {
            var client = new JsonServiceClient(BaseUri);

            Exception ex = Assert.Throws<ServiceStack.ServiceClient.Web.WebServiceException>(() => client.Get<List<MapData>>("/mapdata/blsdsdsah"));
        }

        public void Dispose()
        {
            if (_appHost != null)
                _appHost.Dispose();
            _appHost = null;
        }

        class AppHost : AppHostHttpListenerBase
        {
            public AppHost() : base("MapDataService Tests", typeof(MapDataService).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", false,                    ServiceStack.OrmLite.Sqlite.SqliteOrmLiteDialectProvider.Instance));
                var dbFactory = container.Resolve<IDbConnectionFactory>();

                if (!dbFactory.Run(db => db.TableExists("MapData")))
                {
                    dbFactory.Run(db => db.CreateTableIfNotExists<MapData>());
                    dbFactory.Run(db => db.InsertAll(SeedData.MapData()));
                }
            }
        }
    }
}
