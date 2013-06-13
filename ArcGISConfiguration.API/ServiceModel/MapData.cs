using ArcGIS.ServiceModel.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;

namespace ArcGISConfiguration.API.ServiceModel
{
    [Api("Map configuration data service")]    
    [Route("/mapdata/{Role}", 
        "GET", 
        Summary = "Get a specific map configuration", 
        Notes = "Returns the map configuration data where the role matches the value passed in.")]
    public class MapData 
    {
        public MapData()
        {
            Layers = new List<MapLayer>();
        }

        [AutoIncrement]
        public int Id { get; set; }

        public Extent Extent { get; set; }

        public ServiceStack.Text.JsonObject Options { get; set; }

        [ApiMember(AllowMultiple = false, DataType= "string", Description = "Name of the role used to match data against.", ParameterType = "path", IsRequired = true)]
        public String Role { get; set; }

        public string ProxyUrl { get; set; }

        public List<MapLayer> Layers { get; set; }
    }
}
