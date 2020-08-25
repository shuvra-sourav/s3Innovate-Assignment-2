using System;

namespace TimeSeriesDataApi.Dto
{
    public class TimeseriesDataRequestDto
    {
        public int BuildingId { get; set; }
        public int BuildingObjectId { get; set; }
        public int DataFieldId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}