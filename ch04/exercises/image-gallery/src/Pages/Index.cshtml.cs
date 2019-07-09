using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ImageGallery.Models;

namespace ImageGallery.Pages
{
    public class IndexModel : PageModel
    {
        private static HttpClient _HttpClient;
        private readonly IConfiguration _config;

        public string ImageUrl {get; private set;}
        public string Caption {get; private set;}
        public string Copyright {get; private set;}

        public IndexModel(IConfiguration config)
        {
            _config = config;
            _HttpClient = new HttpClient();
            _HttpClient.DefaultRequestHeaders.Accept.Clear();
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task OnGetAsync()
        {
            var responseBody = await _HttpClient.GetStringAsync(_config["ImageService:Url"]);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var image = JsonSerializer.Parse<Image>(responseBody, options);
            ImageUrl = image.Url;
            Caption = image.Caption;
            Copyright = image.Copyright;
            
            var accessLog = new AccessLog 
            {
                ClientIp = "1.2.3.4"
            };
            var json = JsonSerializer.ToString(accessLog);
            var buffer = Encoding.UTF8.GetBytes(json);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _HttpClient.PostAsync(_config["AccessLogService:Url"], content);
        }
    }
}
