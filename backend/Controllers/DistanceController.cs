using backend.Classes;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/distance")]
    public class DistanceController : ControllerBase
    {
        [HttpGet]
        [Route("by-province")]
        public IActionResult GetDistanceByProvince()
        {
            var data = from province in Data.GetProvinces()
                       join site in Data.GetSites() on province.Id equals site.ProvinceId
                       join driver in Data.GetDrivers() on site.Id equals driver.SiteId
                       join distance in Data.GetDistances() on driver.Id equals distance.DriverId
                       group distance by new { province.Id, province.Name } into result
                       select new
                       {
                           ProvinceId = result.Key.Id,
                           ProvinceName = result.Key.Name,
                           TotalDistanceTravelled = result.Sum(d => d.DistanceTravelled)
                       };

            return data.Count() >= 1 ? Ok(data) : NotFound();
        }

        [HttpGet]
        [Route("by-site/{provinceId}")]
        public IActionResult GetDistanceBySite(int provinceId)
        {
            var data = from site in Data.GetSites()
                       join driver in Data.GetDrivers() on site.Id equals driver.SiteId
                       join distance in Data.GetDistances() on driver.Id equals distance.DriverId
                       where site.ProvinceId == provinceId
                       group distance by new { site.Id, site.Name } into result
                       select new
                       {
                           SiteId = result.Key.Id,
                           SiteName = result.Key.Name,
                           TotalDistanceTravelled = result.Sum(d => d.DistanceTravelled)
                       };

            return data.Count() >= 1 ? Ok(data) : NotFound();
        }

        [HttpGet]
        [Route("by-driver/{siteId}")]
        public IActionResult GetDistanceByDriver(int siteId)
        {
            var data = from driver in Data.GetDrivers()
                       join distance in Data.GetDistances() on driver.Id equals distance.DriverId
                       where driver.SiteId == siteId
                       group distance by new { driver.Id, driver.Name } into result
                       select new
                       {
                           DriverId = result.Key.Id,
                           DriverName = result.Key.Name,
                           TotalDistanceTravelled = result.Sum(d => d.DistanceTravelled)
                       };

            return data.Count() >= 1 ? Ok(data) : NotFound();
        }
    }
}