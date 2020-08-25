using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeseriesDatabase;
using TimeseriesDatabase.Model;

namespace TimeSeriesDataApi.Service
{
    public class TimeSeriesService : ITimeSeriesService
    {
        private TimeSeriesDbContext _timeSeriesDbContext;

        public TimeSeriesService(TimeSeriesDbContext timeSeriesDbContext)
        {
            _timeSeriesDbContext = timeSeriesDbContext;
        }

        public async Task<List<Building>> GetBuilding()
        {
            try
            {
                var values = await _timeSeriesDbContext.Building.ToListAsync();
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public async Task<List<BuildingObject>> GetObject()
        {
            try
            {
                var values = await _timeSeriesDbContext.BuildingObject.ToListAsync();
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<List<BuildingDataField>> GetDataField()
        {
            try
            {
                var values = await _timeSeriesDbContext.BuildingDataFileld.ToListAsync();
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Reading>> GetReading(DateTime startDate, DateTime endDate, int buildingId, int objectId,
            int dataFieldId)
        {
            try
            {
                var values = await _timeSeriesDbContext.Reading.Where(e =>
                    (e.Timestamp >= startDate && e.Timestamp <= endDate) && e.BuildingEntityId == buildingId &&
                    e.ObjectEntityId == objectId && e.DataFieldEntityId == dataFieldId).OrderBy(e=>e.Timestamp).ToListAsync();
                return values;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}