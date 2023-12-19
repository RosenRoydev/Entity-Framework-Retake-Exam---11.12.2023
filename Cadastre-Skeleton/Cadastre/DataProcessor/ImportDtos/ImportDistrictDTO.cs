using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("District")]
    public class ImportDistrictDTO
    {
        [EnumDataType(typeof(Region))]
        [XmlAttribute("Region")]
        public string Region {  get; set; } = null!;

        [Required]
        [MinLength(DataValidators.DataValidators.DistrictNameMinLenght)]
        [MaxLength(DataValidators.DataValidators.DistrictNameMaxLenght)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z]{2}-\d{5})")]
        [XmlElement("PostalCode")]
        public string PostalCode { get; set; } = null!;

        [XmlArray("Properties")]
        public ImportPropertiesDTO[] Properties { get; set; } = null!;
    }
}
//< Districts >

//    < District Region = "SouthWest" >

//        < Name > Sofia </ Name >

//        < PostalCode > SF - 10000 </ PostalCode >

//        < Properties >

//            < Property >

//                < PropertyIdentifier > SF - 10000.001.001.001 </ PropertyIdentifier >

//                < Area > 71 </ Area >

//                < Details > One - bedroom apartment </ Details >

//                < Address > Apartment 5, 23 Silverado Street, Sofia</Address>
//				<DateOfAcquisition>15/03/2022</DateOfAcquisition>
//			</Property>			
