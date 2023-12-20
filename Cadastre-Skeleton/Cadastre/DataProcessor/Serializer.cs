using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Cadastre.Data;
using Cadastre.DataProcessor.ExportDtos;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            string date = "01/01/2000";
            DateTime searchedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           var properties = dbContext.Properties.Where(p => p.DateOfAcquisition >= searchedDate).
                OrderByDescending(p => p.DateOfAcquisition).ThenBy(p => p.PropertyIdentifier)
                .Select(p => new
                {
                    PropertyIdentifier= p.PropertyIdentifier,
                    Area = p.Area,
                    Address = p.Address,
                    DateOfAcquisition= p.DateOfAcquisition.ToString("dd/MM/yyyy"),
                    Owners = p.PropertiesCitizens.Select(ps => new
                    {
                       LastName= ps.Citizen.LastName,
                        MaritalStatus = ps.Citizen.MaritalStatus.ToString(),

                    }).OrderBy(ps => ps.LastName).ToArray(),
                }).ToArray ();

            return JsonConvert.SerializeObject(properties,Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(ExportPropertiesDTO[]), new XmlRootAttribute("Properties"));

            var properties = dbContext.Properties.Where(p => p.Area >= 100)
                .OrderByDescending(p => p.Area).ThenBy(p => p.DateOfAcquisition)
                .Select(p => new ExportPropertiesDTO
                {
                    PostalCode = p.District.PostalCode,
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,                    
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy")
                }).ToArray();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, properties, ns);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
