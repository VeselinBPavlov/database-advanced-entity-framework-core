namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class ExportCategoryByProductCountDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("count")]
        public int ProductCount { get; set; }

        [XmlElement("averagePrice")]
        public decimal AveragePrice { get; set; }

        [XmlElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
