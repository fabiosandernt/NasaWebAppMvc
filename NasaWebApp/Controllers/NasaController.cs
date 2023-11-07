using Microsoft.AspNetCore.Mvc;
using NasaWebApp.Models;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace NasaWebApp.Controllers
{
    public class NasaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NasaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetPictureByDay()
        {
                       
            string apiKey = "3UIuBJm9G4cgV0mcuq6P89Fwi4lReRyAsGW1xh5f";

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apiKey}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(content);
                    var root = jsonDocument.RootElement;

                    var imageUrl = root.GetProperty("url").GetString();
                    var title = root.GetProperty("title").GetString();

                    ViewBag.ImageUrl = imageUrl;
                    ViewBag.Title = title;

                    return View("Nasa");
                }
                else
                {
                    return null;
                }

              
            }
            catch (HttpRequestException)
            {

                return NotFound("Error");
            }
        }
                
    }    
}
