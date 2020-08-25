using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSeriesDataApi.Dto;
using TimeSeriesDataApi.Service;

namespace TimeSeriesDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeSeriesDataController
    {
        private readonly ITimeSeriesService _timeSeriesService;

        public TimeSeriesDataController(ITimeSeriesService timeSeriesService)
        {
            _timeSeriesService = timeSeriesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTimeSeriesData([FromBody]TimeseriesDataRequestDto requestDto)
        {
            var readingData = await _timeSeriesService.GetReading(requestDto.StartDate,requestDto.EndDate,requestDto.BuildingId,requestDto.BuildingObjectId,requestDto.DataFieldId);
            if (readingData == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(readingData);
        }
        
    }
}