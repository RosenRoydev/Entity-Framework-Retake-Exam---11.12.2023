using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastre.DataValidators;
namespace Cadastre.Data.Models
{
    public class Property
    {
        public Property() 
        { 
         PropertiesCitizens = new List<PropertyCitizen>();  
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidators.DataValidators.PropertyIdentifierMaxLenght)]
        [MinLength(DataValidators.DataValidators.PropertyIdentifierMinLenght)]
        public string PropertyIdentifier { get; set; } = null!;

        [Required]
        [Range(0,int.MaxValue)]
        public int Area { get; set; }

        
        [MaxLength(DataValidators.DataValidators.DetailsMaxLenght)]
        [MinLength(DataValidators.DataValidators.DetailsMinLenght)]
        public string? Details { get; set; }

        [Required]
        [MinLength(DataValidators.DataValidators.AddressMinLenght)]
        [MaxLength(DataValidators.DataValidators.AddressMaxLenght)]
        public string Address { get; set; } = null!;

        [Required]
        public DateTime DateOfAcquisition { get; set; }

        [Required]
        [ForeignKey(nameof(DistrictId))]
        public int DistrictId { get; set; }

        public District District { get; set; }

        public ICollection<PropertyCitizen> PropertiesCitizens {  get; set; }
    }
}
//•	Id – integer, Primary Key
//•	PropertyIdentifier – text with length [16, 20] (required)
//•	Area – int not negative (required)
//•	Details - text with length [5, 500] (not required)
//•	Address – text with length [5, 200] (required)
//•	DateOfAcquisition – DateTime (required)
//•	DistrictId – integer, foreign key (required)
//•	District – District
//•	PropertiesCitizens - collection of type PropertyCitizen
