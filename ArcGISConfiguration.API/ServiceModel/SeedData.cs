using System.Collections.Generic;
using ArcGIS.ServiceModel.Common;

namespace ArcGISConfiguration.API.ServiceModel
{
    public static class SeedData
    {
        public static List<MapData> MapData()
        {
            return new List<MapData> 
            {     
                new MapData
                {
                    Role = "basemap",
                    Extent = new Extent
                    { 
                        SpatialReference = new SpatialReference
                        { 
                            Wkid = 2193
                        }, 
                        XMin = 1704801.749880849, 
                        YMin = 5404709.595556739, 
                        XMax = 1789799.315709314, 
                        YMax = 5454517.507672562
                    },
                    Options = new ServiceStack.Text.JsonObject
                    {
                        {"wrapAround180", "false"}
                    },
                    Layers = new List<MapLayer> 
                    {
                        new MapLayer
                        {
                            Type = MapLayerType.ArcGISTiledMapServiceLayer,
                            Url =  @"http://services.arcgisonline.co.nz/arcgis/rest/services/Generic/newzealand/MapServer"
                        }
                    }                        
                },
                new MapData
                {
                    Role = "operational",
                    ProxyUrl = "proxy.ashx",
                    Extent = new Extent
                    { 
                        SpatialReference = new SpatialReference
                        { 
                            Wkid = 102100
                        }, 
                        XMin = -16803916.29820868, 
                        YMin = 562576.5281787475, 
                        XMax = -4231553.885866234, 
                        YMax = 7929883.062415216
                    },                    
                    Options = new ServiceStack.Text.JsonObject
                    {
                        {"zoom", "4"}
                    },
                    Layers = new List<MapLayer> 
                    {
                        new MapLayer
                        {
                            Type = MapLayerType.ArcGISTiledMapServiceLayer,
                            Url =  @"http://services.arcgisonline.com/ArcGIS/rest/services/Canvas/World_Light_Gray_Base/MapServer"
                        },
                        new MapLayer
                        {
                            Type = MapLayerType.ArcGISTiledMapServiceLayer,
                            Url =  @"http://services.arcgisonline.com/ArcGIS/rest/services/Demographics/USA_Tapestry/MapServer",
                            Options = new ServiceStack.Text.JsonObject
                            {  
                                {"id", "Demographics"}, 
                                {"opacity", "0.25" },
                                {"visible", "true" }
                            }
                        },
                        new MapLayer
                        {
                            Type = MapLayerType.ArcGISDynamicMapServiceLayer,
                            Url =  @"http://services.arcgisonline.co.nz/arcgis/rest/services/STATS/territorialauthorities/MapServer",
                            Options = new ServiceStack.Text.JsonObject
                            {
                                {"id", "Data layer"}
                            }
                        }
                    }  
                }                    
            };
        }
    }
}
