using System;

namespace TimeSeriesDataApi.Dto
{
    public class TimeseriesDataRequestDto
    {
        public int BuildingId { get; set; }
        public int BuildingObjectId { get; set; }
        public int DataFieldId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}