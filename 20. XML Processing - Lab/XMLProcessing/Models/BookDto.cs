namespace XMLProcessing.Models
{
    using System.Xml.Serialization;

    [XmlType("Book")]
    public class BookDto
    {
        [XmlElement("BookName")]
        public string Name { get; set; }

        [XmlText]
        public string Description { get; set; }

        [XmlElement("BookAuthor")]
        public string Author { get; set; }
    }
}