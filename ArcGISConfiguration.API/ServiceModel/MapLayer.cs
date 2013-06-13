using System.ComponentModel.DataAnnotations;

namespace ArcGISConfiguration.API.ServiceModel
{
    public class MapLayer
    {
        [Required]
        public MapLayerType Type { get; set; }

        [Required]
        public string Url { get; set; }

        public ServiceStack.Text.JsonObject Options { get; set; }
    }
}
