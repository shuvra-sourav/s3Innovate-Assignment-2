using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSeriesDataApi.Dto;
using TimeSeriesDataApi.Service;
using TimeseriesDatabase.Model;

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
        [HttpPost]
        public async Task<IActionResult> GetTimeSeriesData([FromBody]TimeseriesDataRequestDto requestDto)
        {
            DateTime startDate = DateTime.ParseExact(requestDto.StartDate, "M/d/yyyy, h:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(requestDto.EndDate, "M/d/yyyy, h:mm:ss tt", CultureInfo.InvariantCulture);
            var readingData = await _timeSeriesService.GetReading(startDate,endDate,requestDto.BuildingId,requestDto.BuildingObjectId,requestDto.DataFieldId);
            
            if (readingData == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(readingData);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStartAndEndDate()
        {
            var dateLimit = await _timeSeriesService.GetDateLimits();
            if (dateLimit.startDate == null && dateLimit.endDate == null)
            {
                return new NotFoundResult();
            }
            var dateData = new DateLimitResponseDto()
            {
                StartDate = dateLimit.startDate==null?dateLimit.endDate.AddYears(-2):dateLimit.startDate,
                EndDate = dateLimit.endDate==null?dateLimit.startDate.AddYears(2):dateLimit.endDate
            };

            return new OkObjectResult(dateData);
        }
    }
}