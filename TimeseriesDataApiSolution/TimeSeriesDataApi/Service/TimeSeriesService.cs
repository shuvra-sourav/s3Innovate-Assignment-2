using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSeriesDataApi.Dto;
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

        public async Task<(DateTime startDate, DateTime endDate)> GetDateLimits()
        {
            var startDate = await _timeSeriesDbContext.Reading.MinAsync(e => e.Timestamp);
            var endDate= await _timeSeriesDbContext.Reading.MaxAsync(e => e.Timestamp);
            return (startDate, endDate);
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

        public async Task<List<TimeseriesDataResponseDto>> GetReading(DateTime startDate, DateTime endDate, int buildingId, int objectId,
            int dataFieldId)
        {
            try
            {
                var values = await _timeSeriesDbContext.Reading.Where(e =>
                    (e.Timestamp >= startDate && e.Timestamp <= endDate) && e.BuildingEntityId == buildingId &&
                    e.ObjectEntityId == objectId && e.DataFieldEntityId == dataFieldId).OrderBy(e=>e.Timestamp).Select(e=>new TimeseriesDataResponseDto()
                {
                    Timestamp = e.Timestamp,
                    DataValue = e.Value
                }).ToListAsync();
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