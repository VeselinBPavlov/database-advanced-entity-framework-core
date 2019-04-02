namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class ExportCustomUserProductDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUserAndProductDto[] ExportUserAndProductDtos { get; set; }
    }
}
