namespace XMLProcessing.Models
{
    using System.Xml.Serialization;

    [XmlType("Section")]
    public class SectionDto
    {
        [XmlElement("SectionName")]
        public string Name { get; set; }

        [XmlArrayItem("book")]
        public BookDto[] Books { get; set; }
    }
}