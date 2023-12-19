using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("Property")]
    public class ImportPropertiesDTO
    {

        [Required]
        [MaxLength(DataValidators.DataValidators.PropertyIdentifierMaxLenght)]
        [MinLength(DataValidators.DataValidators.PropertyIdentifierMinLenght)]
        [XmlElement("PropertyIdentifier")]
        public string PropertyIdentifier { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [XmlElement("Area")]
        public int Area { get; set; }

        [Required]
        [MaxLength(DataValidators.DataValidators.DetailsMaxLenght)]
        [MinLength(DataValidators.DataValidators.DetailsMinLenght)]
        [XmlElement("Details")]
        public string Details { get; set; }

        [Required]
        [MinLength(DataValidators.DataValidators.AddressMinLenght)]
        [MaxLength(DataValidators.DataValidators.AddressMaxLenght)]
        [XmlElement("Address")]
        public string Address { get; set; }

        [Required]
        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }
    }
}
//< Property >

//                < PropertyIdentifier > SF - 10000.001.001.001 </ PropertyIdentifier >

//                < Area > 71 </ Area >

//                < Details > One - bedroom apartment </ Details >

//                < Address > Apartment 5, 23 Silverado Street, Sofia</Address>
//				<DateOfAcquisition>15/03/2022</DateOfAcquisition>