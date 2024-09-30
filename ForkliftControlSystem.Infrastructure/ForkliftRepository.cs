using CsvHelper;
using ForkliftControlSystem.Domain.Entities;
using ForkliftControlSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkliftControlSystem.Infrastructure
{
    public class ForkliftRepository : IForkliftRepository
    {
        public async Task<List<Forklift>> GetAllForklifts(string filePath)
        {
            var forklifts = new List<Forklift>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    var forklift = new Forklift
                    {
                        Name = csv.GetField("Name"),
                        ModelNumber = csv.GetField("Model Number"),
                        ManufacturingDate = DateTime.Parse(csv.GetField("Manufacturing Date"))
                    };
                    forklifts.Add(forklift);
                }
            }
            return forklifts;
        }
    }
}
