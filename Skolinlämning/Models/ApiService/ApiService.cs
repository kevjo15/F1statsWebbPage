using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Skolinlämning.Models;
using System.Collections.Generic;


public class ApiService
{
    private readonly HttpClient _httpClient;
    internal object httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return await _httpClient.SendAsync(request);
    }


    public async Task<IEnumerable<Driver>> GetDriversAsync()
    {
        var response = await _httpClient.GetAsync("https://ergast.com/api/f1/2023/drivers.json");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

        return apiResponse.MRData.DriverTable.Drivers;
    }






}
