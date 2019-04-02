namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("User")]
    public class ExportUserAndProductDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductDto SoldProductDto { get; set; }
    }

    public class SoldProductDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ProductDto[] Products { get; set; }
    }

    [XmlType("Product")]
    public class ProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
