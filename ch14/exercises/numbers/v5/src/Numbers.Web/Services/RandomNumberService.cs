using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Numbers.Web.Services
{
    public class RandomNumberService
    {
        private readonly IConfiguration _config;

        public RandomNumberService(IConfiguration config)
        {
            _config = config;
        }

        public int GetNumber()
        {
            var client = new RestClient(_config["RngApi:Url"]);
            var request = new RestRequest();
            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Service call failed");
            }
            return int.Parse(response.Content);
        }
    }
}
