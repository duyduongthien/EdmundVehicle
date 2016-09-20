using System.Net.Http.Headers;
using System.Text;
using EdmundVehicle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EdmundVehicle.Controllers
{
    public class MakesViewModel
    {
        public IEnumerable<MakeAPI> Makes { get; set; }
        public int Count { get; set; }
    }

    public class MakeAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public ICollection<ModelAPI> Models { get; set; }
    }

    public class ModelAPI
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
    }

    public class VehicleController : ApiController
    {
        private string _apiKey;
		private string _apiRoot;
		private HttpClient _client;

		public VehicleController()
		{
            _apiKey = "x8xzvmzzsftvmdgscar3w6bx";
			_apiRoot = "api/vehicle/v2/";

			_client = new HttpClient();
			_client.BaseAddress = new Uri( "http://api.edmunds.com/" );
			_client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
		}

        public async Task<IEnumerable<MakeAPI>> GetAllMakes()
		{
			IDictionary<string, string> parameters = null;

			HttpResponseMessage response = await _client.GetAsync( GenerateURLForResource("makes", parameters) );
			response.EnsureSuccessStatusCode();

            var responseBody = JsonConvert.DeserializeObject<MakesViewModel>(await response.Content.ReadAsStringAsync());
			return responseBody.Makes;
		}


        private string GenerateURLForResource(string resource, IDictionary<string, string> options = null)
        {
            StringBuilder url = new StringBuilder();
            url.Append(_apiRoot);
            url.Append(resource);
            url.Append("?");
            url.Append("api_key=" + _apiKey);

            if (options != null)
            {
                url.Append("&");
                url.Append(string.Join("&", options.Select(entry => entry.Key + "=" + entry.Value).ToArray()));
            }

            return url.ToString();
        }
    }
}
