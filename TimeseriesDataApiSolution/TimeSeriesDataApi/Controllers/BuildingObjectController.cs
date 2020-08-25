using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSeriesDataApi.Service;

namespace TimeSeriesDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildingObjectController
    {
        private readonly ITimeSeriesService _timeSeriesService;

        public BuildingObjectController(ITimeSeriesService timeSeriesService)
        {
            _timeSeriesService = timeSeriesService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetObject()
        {
            var buildingObject = await _timeSeriesService.GetObject();
            if (buildingObject == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(buildingObject);
        }
    }
}