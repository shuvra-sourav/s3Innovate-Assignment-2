using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeSeriesDataApi.Service;

namespace TimeSeriesDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildingController
    {
        private readonly ITimeSeriesService _timeSeriesService;

        public BuildingController(ITimeSeriesService timeSeriesService)
        {
            _timeSeriesService = timeSeriesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBuilding()
        {
            var building = await _timeSeriesService.GetBuilding();
            if (building == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(building);
        }
    }
}