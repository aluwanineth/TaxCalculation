using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;
using TaxCalculations.Application.Wrappers;
using TaxCalculations.ClientApp.Models;

namespace TaxCalculations.ClientApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var baseUrl = _configuration.GetValue<string>("APIBaseUrl");
        var endpoint = baseUrl + "TaxCalculations/getTaxCalculations";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        
        using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Response<IEnumerable<TaxCalculateQueryResponse>>>(jsonString);
            // ViewBag.result = results.Message;
            return View(results.Result);
        }
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public  async Task<IActionResult> Create(TaxCalculateCommand model)
    {
        if (ModelState.IsValid)
        {
            var baseUrl = _configuration.GetValue<string>("APIBaseUrl");
            var endpoint = baseUrl + "TaxCalculations/calculateTax";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string jsonBody = JsonConvert.SerializeObject(model);
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<Response<TaxCalculateCommandResponse>>(jsonString);
                ViewBag.taxResults = results.Message;
                ViewBag.error = string.Empty;
            }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<Response<TaxCalculateCommandResponse>>(jsonString);
                ViewBag.error = results.Message;
                ViewBag.taxResults = string.Empty;
            }
        }
        return View(model);
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await GetAllTaxCalculation(id);
        return View(result);
    }

    [HttpDelete, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var baseUrl = _configuration.GetValue<string>("APIBaseUrl");
        var endpoint = $"{baseUrl}TaxCalculations/deleteTaxCalculation/{id}";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);


        HttpResponseMessage response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await GetAllTaxCalculation(id);
        return View(result);
    }

    private async Task<GetTaxCalculationByIdQueryResponse> GetAllTaxCalculation(Guid id)
    {
        var apiResults = new GetTaxCalculationByIdQueryResponse();
        var baseUrl = _configuration.GetValue<string>("APIBaseUrl");
        var endpoint = $"{baseUrl}TaxCalculations/getTaxCalculation/{id}";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);


        HttpResponseMessage response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Response<GetTaxCalculationByIdQueryResponse>>(jsonString);
            // ViewBag.result = results.Message;
            return results.Result;
        }
        else
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Response<GetTaxCalculationByIdQueryResponse>>(jsonString);
            // ViewBag.result = results.Message;
            return results.Result;
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}