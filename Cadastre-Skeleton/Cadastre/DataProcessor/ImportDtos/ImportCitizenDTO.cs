using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastre.Data.Enumerations;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor.ImportDtos
{
    public class ImportCitizenDTO
    {
        [Required]
        [MinLength(DataValidators.DataValidators.FirstNameMinLenght)]
        [MaxLength(DataValidators.DataValidators.FirstNameMaxLenght)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(DataValidators.DataValidators.LastNameMinLenght)]
        [MaxLength(DataValidators.DataValidators.LastNameMaxLenght)]
        [JsonProperty("LastName")]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(MaritalStatus))]
        [JsonProperty("MaritalStatus")]
        public string MaritalStatus { get; set; } = null!;

        [JsonProperty("Properties")]
        public int[] Properties { get; set; } = null!;
    }
}
//{
//    "FirstName": "Ivan",
//    "LastName": "Georgiev",
//    "BirthDate": "12-05-1980",
//    "MaritalStatus": "Married",
//    "Properties": [ 17, 29 ]
//  },
