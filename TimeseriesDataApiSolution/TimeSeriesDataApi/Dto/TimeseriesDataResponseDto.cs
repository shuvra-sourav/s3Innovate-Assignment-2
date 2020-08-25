using System;

namespace TimeSeriesDataApi.Dto
{
    public class TimeseriesDataResponseDto
    {
        public DateTime Timestamp { get; set; }
        public decimal DataValue { get; set; }
    }
}