using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSeriesDataApi.Service;

namespace TimeSeriesDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFieldController
    {
        private readonly ITimeSeriesService _timeSeriesService;

        public DataFieldController(ITimeSeriesService timeSeriesService)
        {
            _timeSeriesService = timeSeriesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDataField()
        {
            var dataField = await _timeSeriesService.GetDataField();
            if (dataField == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(dataField);
        }
        
    }
}