using System.Runtime.Serialization;

namespace ArcGISConfiguration.API.ServiceModel
{
    [DataContract]
    public class Extent
    {
        [DataMember(Order = 5)]
        public SpatialReference SpatialReference { get; set; }

        [DataMember(Name = "xmin", Order = 1)]
        public double XMin { get; set; }

        [DataMember(Name = "xmax", Order = 3)]
        public double XMax { get; set; }

        [DataMember(Name = "ymin", Order = 2)]
        public double YMin { get; set; }

        [DataMember(Name = "ymax", Order = 4)]
        public double YMax { get; set; }
    }
}
