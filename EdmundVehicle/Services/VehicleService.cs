using EdmundVehicle.Models;
using EdmundVehicle.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EdmundVehicle.Services
{
    public interface IVehicleService
    {
        bool UpdateVehicleDatabase();
        // void GetMakesWithModel();
        // void Get
    }

    public class EdmundMakes
    {
        public IEnumerable<EdmundMake> Makes { get; set; }
        public int Count { get; set; }
    }

    public class EdmundMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public ICollection<EdmundModel> Models { get; set; }
    }

    public class EdmundModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
    }

    public class VehicleService : IVehicleService
    {
        private string apiKey;
        private string apiRoot;
        private HttpClient client;
        private IBaseRepository<Make> makeRepository;
        private IBaseRepository<Model> modelRepository; 

        public VehicleService(IBaseRepository<Make> makeRepository, IBaseRepository<Model> modelRepository)
        {
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
            apiKey = "x8xzvmzzsftvmdgscar3w6bx";
            apiRoot = "api/vehicle/v2/";

            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.edmunds.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool UpdateVehicleDatabase()
        {
            // Get Makes with Models associated asynchronously from Edmund API
            var makes = GetMakeModelFromEdmund();
            if (makes.IsCompleted && makes.Result !=null && makes.Result.Any())
            {
                try
                {
                    //Select all Makes available in our database
                    var currentMakes = makeRepository.GetAll();
                    //Delete all Makes - cascasde delete all Models
                    if (currentMakes!=null && currentMakes.Any())
                        makeRepository.DeleteMulti(currentMakes);
                    //Insert new Makes 

                    //makeRepository.InsertMulti()
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                // return false if no result returned from Edmund API
                return false;
            }
        }

        public async Task<IEnumerable<EdmundMake>> GetAllMakes()
        {
            IDictionary<string, string> parameters = null;

            HttpResponseMessage response = await client.GetAsync(GenerateURLForResource("makes", parameters));
            response.EnsureSuccessStatusCode();

            var responseBody = JsonConvert.DeserializeObject<EdmundMakes>(await response.Content.ReadAsStringAsync());
            return responseBody.Makes;
        }

        public async Task<IEnumerable<Make>> GetMakeModelFromEdmund()
        {
            IDictionary<string, string> parameters = null;

            HttpResponseMessage response = await client.GetAsync(GenerateURLForResource("makes", parameters));
            response.EnsureSuccessStatusCode();

            var responseBody = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var makes = responseBody.makes;
            if (makes != null && makes.count > 0)
            {
                
            }
            return null;
        }

        private string GenerateURLForResource(string resource, IDictionary<string, string> options = null)
        {
            StringBuilder url = new StringBuilder();
            url.Append(apiRoot);
            url.Append(resource);
            url.Append("?");
            url.Append("api_key=" + apiKey);

            if (options != null)
            {
                url.Append("&");
                url.Append(string.Join("&", options.Select(entry => entry.Key + "=" + entry.Value).ToArray()));
            }

            return url.ToString();
        }
    }
}