using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeseriesDatabase;
using TimeseriesDatabase.Model;

namespace SampleDataConsoleApp
{
    public class CreateSampleData:ICreateSampleData
    {
        Random rnd = new Random();
        private const int buildingNumber = 100;
        private const  int objectNumber = 1;
        private const int dataFieldNumber = 1;
        private static readonly DateTime startDate = new DateTime(2018,1,1,12,0,0);
        private static readonly DateTime endDate = new DateTime(2020,1,1,12,0,0);
        private readonly TimeSeriesDbContext _timeSeriesDbContext ;

        public CreateSampleData(TimeSeriesDbContext timeSeriesDbContext)
        {
            _timeSeriesDbContext = timeSeriesDbContext;
        }


        public async Task GenerateSampleData()
        {

            await addBuildingToDatabase();
            Console.WriteLine("Added building data");
            await addObjectToDatabase();
            Console.WriteLine("Added object data");
            await addDataFieldToDatabase();
            Console.WriteLine("Added data field data");
            await addReadingToDatabase();
            Console.WriteLine("Added reading data");
        }
        
        
        
        private async Task addObjectToDatabase()
        {
            var list = new List<BuildingObject>();
            for (int i = 0; i < objectNumber; i++)
            {
                var item = generateObject();
                item.Id = Convert.ToByte( i + 1);
                list.Add(item);
            } 
            await _timeSeriesDbContext.AddRangeAsync(list);
            await _timeSeriesDbContext.SaveChangesAsync();
            
        }
        
        
        private async Task addDataFieldToDatabase()
        {
            var list = new List<BuildingDataField>();
            for (int i = 0; i < dataFieldNumber; i++)
            {
                var item = generateDataField();
                item.Id = Convert.ToByte( i + 1);
                list.Add(item);
            } 
            await _timeSeriesDbContext.AddRangeAsync(list);
            await _timeSeriesDbContext.SaveChangesAsync();
        }
        private async Task addReadingToDatabase()
        {
            var buildingIds = await _timeSeriesDbContext.Building.Select(e => e.Id).ToArrayAsync();
            var objectIds = await _timeSeriesDbContext.BuildingObject.Select(e => e.Id).ToArrayAsync();
            var dataFieldIds = await _timeSeriesDbContext.BuildingDataFileld.Select(e => e.Id).ToArrayAsync();
            int i = 1;
            
            foreach (var buildingId in buildingIds)
            {
                foreach (var objectId in objectIds)
                {
                    foreach (var dataFieldId in dataFieldIds)
                    {
                        for (DateTime currentTimestamp = startDate; currentTimestamp <= endDate; currentTimestamp = currentTimestamp.AddMonths(1))
                        {
                            var monthlyData = getReading(currentTimestamp,currentTimestamp.AddMonths(1),buildingId,objectId,dataFieldId);
                            await _timeSeriesDbContext.AddRangeAsync(monthlyData);
                            await _timeSeriesDbContext.SaveChangesAsync();
                        }
                    }
                }
                Console.WriteLine($"Added for building {i++}");
            }
        }

        private List<Reading> getReading(DateTime startDate, DateTime endDate,short buildingId, byte ObjectEntityId, byte DataFieldEntityId)
        {
            var list = new List<Reading>();
            for (DateTime currentTimestamp = startDate; currentTimestamp < endDate; currentTimestamp = currentTimestamp.AddSeconds(6))
            {
                var item = generateReading();
                item.BuildingEntityId = buildingId;
                item.ObjectEntityId = ObjectEntityId;
                item.DataFieldEntityId = DataFieldEntityId;
                item.Timestamp = new DateTime(currentTimestamp.Year,currentTimestamp.Month,currentTimestamp.Day,currentTimestamp.Hour,currentTimestamp.Minute,currentTimestamp.Second);
                list.Add(item);
            }

            return list;
        }

        private async Task addBuildingToDatabase()
        {
            var list = new List<Building>();
            for (int i = 0; i < buildingNumber; i++)
            {
                var item = generateBuilding();
                item.Id = Convert.ToByte( i + 1);
                list.Add(item);
            }
            await _timeSeriesDbContext.AddRangeAsync(list);
            await _timeSeriesDbContext.SaveChangesAsync();
        }
        
        private Reading generateReading()
        {
            return new Reading()
            {
                Value = Convert.ToDecimal(rnd.NextDouble()*100)
            };
        }
        private Building generateBuilding()
        {
            var locations = new string[]
            {"Asia","Europe","USA","Australlia","Africa"};
            int mIndex = rnd.Next(locations.Length);
            return new Building()
            {
                Location = locations[mIndex]==null ? locations[mIndex]: string.Empty,
                Name = generateString(rnd.Next(10,45))
            };
        }
        
        private BuildingObject generateObject()
        {
            return new BuildingObject()
            {
                Name = generateString(rnd.Next(10,45))
            };
        }
        
        private BuildingDataField generateDataField()
        {
            return new BuildingDataField()
            {
                Name = generateString(rnd.Next(10,45))
            };
        }

        private string generateString(int length)
        {
            StringBuilder str_build = new StringBuilder();  
            Random random = new Random();  

            char letter;  

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);  
            }
            return str_build.ToString();
        }
    }
}