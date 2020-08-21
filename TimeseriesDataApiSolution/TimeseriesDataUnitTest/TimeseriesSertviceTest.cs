using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSeriesDataApi.Service;

namespace TimeseriesDataUnitTest
{
    [TestClass]
    public class TimeseriesSertviceTest
    {
        private ITimeseriesService _timeseriesService;
        
        [TestInitialize]
        public virtual void MyTestInitialize()
        {
            _timeseriesService = new TimeseriesService();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("test",_timeseriesService.TestService());
        }
    }
}