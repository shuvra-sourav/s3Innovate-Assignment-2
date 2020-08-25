using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeseriesDatabase.Model;

namespace TimeSeriesDataApi.Service
{
    public interface ITimeSeriesService
    {
        Task<List<BuildingDataField>> GetDataField();
        Task<List<BuildingObject>> GetObject();
        Task<List<Building>> GetBuilding();
        
        Task<List<Reading>> GetReading(DateTime startDate, DateTime endDate, int buildingId, int objectId,
            int dataFieldId);
        
    }
}