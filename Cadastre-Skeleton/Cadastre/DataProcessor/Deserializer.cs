namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(ImportDistrictDTO[]), new XmlRootAttribute("Districts"));
            using StringReader reader = new StringReader(xmlDocument);

            ImportDistrictDTO[] importDistrictDTOs = (ImportDistrictDTO[])serializer.Deserialize(reader);
            List<District> districts = new List<District>();

            foreach (var district in importDistrictDTOs)
            {
                if (!IsValid(district))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (dbContext.Districts.Any(d => d.Name == district.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                District validDistrict = new District()
                {
                    Region = (Region)Enum.Parse(typeof(Region), district.Region),
                    Name = district.Name,
                    PostalCode = district.PostalCode
                };

                foreach (ImportPropertiesDTO property in district.Properties)
                {
                    if (!IsValid(property))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (dbContext.Properties.Any(p=> p.PropertyIdentifier == property.PropertyIdentifier) ||
                        validDistrict.Properties.Any(vd => vd.PropertyIdentifier == property.PropertyIdentifier))
                    {
                        sb.Append(ErrorMessage); 
                        continue;
                    }
                    if(dbContext.Properties.Any(p => p.Address == property.Address) || 
                        validDistrict.Properties.Any(vd => vd.Address == property.Address))
                    {
                        sb.Append(ErrorMessage);
                        continue;
                    }
                    
                    Property validProperty = new Property()
                    {
                        PropertyIdentifier = property.PropertyIdentifier,
                        Address = property.Address,
                        Area = property.Area,
                        Details = property.Details,
                        DateOfAcquisition = DateTime.ParseExact(property.DateOfAcquisition, "dd/MM/yyyy",CultureInfo.InvariantCulture)
                    };
                    validDistrict.Properties.Add(validProperty);

                }
                districts.Add(validDistrict);
                sb.AppendLine(String.Format(SuccessfullyImportedDistrict,validDistrict.Name,validDistrict.Properties.Count));
            }
            dbContext.Districts.AddRange(districts);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();
            List<Citizen>citizens = new List<Citizen>();
            ImportCitizenDTO[] importCitizenDTOs = JsonConvert.DeserializeObject<ImportCitizenDTO[]>(jsonDocument);

            foreach (var importCitizen  in importCitizenDTOs)
            {
                if (!IsValid(importCitizen))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen citizen = new Citizen()
                {
                    FirstName = importCitizen.FirstName,
                    LastName = importCitizen.LastName,
                    BirthDate = DateTime.ParseExact(importCitizen.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), importCitizen.MaritalStatus),

                };

                foreach (var propertyId in importCitizen.Properties)
                {
                    citizen.PropertiesCitizens.Add(new PropertyCitizen
                    {
                        PropertyId = propertyId,
                        CitizenId = citizen.Id,
                    });
                }
                citizens.Add(citizen);
                sb.AppendLine(String.Format(SuccessfullyImportedCitizen,citizen.FirstName, citizen.LastName,citizen.PropertiesCitizens.Count));
            }
            dbContext.Citizens.AddRange(citizens);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
