namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartDto[] Parts { get; set; }        
    }

    [XmlType("partId")]
    public class PartDto
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
