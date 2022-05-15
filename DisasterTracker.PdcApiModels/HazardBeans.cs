using System.Xml.Serialization;

namespace DisasterTracker.PdcApiModels
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "hazardBeans", Namespace = "", IsNullable = false)]
    public class HazardBeans
    {

        [XmlElement("hazardBean")]
        public HazardBean[] HazardBean { get; set; }
    }

}
