using Microsoft.AspNetCore.Mvc;
using Skolinlämning.Models;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Skolinlämning.Controllers
{
    [Authorize]
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApiService _apiService;

        public ApiController(HttpClient httpClient)
        {
            _apiService = new ApiService(httpClient);
        }




        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DriverDetails(DriverTable driverTable)
        {
            var drivers = await _apiService.GetDriversAsync();

           
            if (!string.IsNullOrEmpty(driverTable.SearchTerm))
            {
                drivers = drivers.Where(d =>
                    (d.DriverId != null && d.DriverId.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.PermanentNumber != null && d.PermanentNumber.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.Code != null && d.Code.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.Url != null && d.Url.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.GivenName != null && d.GivenName.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.FamilyName != null && d.FamilyName.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.DateOfBirth != null && d.DateOfBirth.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (d.Nationality != null && d.Nationality.Contains(driverTable.SearchTerm, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
            }

            return View(drivers);
        }


        [HttpGet]
        public async Task<IActionResult> RaceDetails()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://ergast.com/api/f1/current.json");
            HttpResponseMessage response = await _apiService.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var apiRaceResponse = JsonConvert.DeserializeObject<ApiRaceResponse>(responseString);
                return View(apiRaceResponse.MRData.RaceTable.Races);
            }

            return View("NotFound");
        }




    }

}

