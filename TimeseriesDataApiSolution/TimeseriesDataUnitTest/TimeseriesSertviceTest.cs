using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeriesDataApi.Service;
using TimeseriesDatabase;

namespace TimeseriesDataUnitTest
{
    [TestClass]
    public class TimeseriesSertviceTest
    {
        protected ITimeSeriesService _timeSeriesService{ get; set; }
        protected TimeSeriesDbContext _databaseContext { get; set; }
        
        private static readonly DateTime startDate = new DateTime(2018,1,1,12,0,0);
        
        [TestInitialize]
        public virtual void MyTestInitialize()
        {
            _databaseContext = new TimeSeriesDbContext();
            _databaseContext.Database.OpenConnection();
            _databaseContext.Database.EnsureCreated();
            _timeSeriesService = new TimeSeriesService(_databaseContext);
        }
        
        [TestMethod]
        public async Task GetBuildingTest()
        {
            var values = await _timeSeriesService.GetBuilding();
            Assert.IsNotNull(values);
        }

        [TestMethod]
        public async Task GetObjectTest()
        {
            var values = await _timeSeriesService.GetObject();
            Assert.IsNotNull(values);
        }

        [TestMethod]
        public async Task GetDataFieldTest()
        {
            var values = await _timeSeriesService.GetDataField();
            Assert.IsNotNull(values);
        }

        [TestMethod]
        public async Task GetReadingTest()
        {
            var values = await _timeSeriesService.GetReading(startDate, startDate.AddMinutes(1), 1, 1, 1);
            Assert.IsNotNull(values);
        }
    }
}