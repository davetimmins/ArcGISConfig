using ArcGISConfiguration.API.ServiceModel;
using ServiceStack.OrmLite;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ArcGISConfiguration.Test
{
    public sealed class MapDataServiceTests : IDisposable
    {
        const string BaseUri = "http://*:2001/";
        TestAppHost _appHost = new TestAppHost();

        public MapDataServiceTests()
        {
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
            Assert.Equal(data.First(), expected);
        }

        [Fact]
        public void MapDataThrowsExceptionForEmptyRole()
        {
            var client = new JsonServiceClient(BaseUri);

            Exception ex = Assert.Throws<WebServiceException>(() => client.Get<List<MapData>>("/mapdata/ /"));
        }

        [Fact]
        public void MapDataThrowsExceptionForUnmatchedRole()
        {
            var client = new JsonServiceClient(BaseUri);

            Assert.Throws<WebServiceException>(() => client.Get<List<MapData>>("/mapdata/blsdsdsah"));
        }

        public void Dispose()
        {
            if (_appHost != null)
                _appHost.Dispose();
            _appHost = null;
        }
    }

    public class TestAppHost : AppHostHttpListenerBase
    {
        public TestAppHost() : base("MapDataService Tests", typeof(TestAppHost).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", false, ServiceStack.OrmLite.Sqlite.SqliteOrmLiteDialectProvider.Instance));
            var dbFactory = container.Resolve<IDbConnectionFactory>();

            if (!dbFactory.Run(db => db.TableExists("MapData")))
            {
                dbFactory.Run(db => db.CreateTableIfNotExists<MapData>());
                dbFactory.Run(db => db.InsertAll(SeedData.MapData()));
            }
        }
    }
}
