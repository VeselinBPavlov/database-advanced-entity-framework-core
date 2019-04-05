namespace SoftJail.DataProcessor.ExportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class ExportPrisonerDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        [Required]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public ExportMessageDto[] EncryptedMessages { get; set; }
    }

    [XmlType("Message")]
    public class ExportMessageDto
    {
        [XmlElement("Description")]
        [Required]
        public string Description { get; set; }

    }
}
