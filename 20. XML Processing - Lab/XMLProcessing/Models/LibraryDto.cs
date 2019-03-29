namespace XMLProcessing.Models
{
    using System.Xml.Serialization;

    [XmlRoot("Library")]
    [XmlType("Library")]
    public class LibraryDto
    {
        [XmlAttribute("Name")]
        public string LibraryName { get; set; }

        [XmlElement("Sections")]
        public SectionDto Sections { get; set; }

        [XmlIgnore]
        public decimal CardPrice { get; set; }
    }
}
