using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastre.Data.Enumerations;
using Cadastre.DataValidators;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cadastre.Data.Models
{
    public  class District
    {
        public District() 
        {
            Properties = new List<Property>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(DataValidators.DataValidators.DistrictNameMinLenght)]
        [MaxLength(DataValidators.DataValidators.DistrictNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z]{2}-\d{5})")]
        public string PostalCode {  get; set; }

        [Required]
        public Region Region { get; set; }

        [Required]
        public ICollection<Property> Properties { get; set; }

    }
}
//•	Id – integer, Primary Key
//•	Name – text with length [2, 80] (required)
//•	PostalCode – text with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits. Example: SF - 10000(required)
//•	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest)(required)
//•	Properties - collection of type Property
