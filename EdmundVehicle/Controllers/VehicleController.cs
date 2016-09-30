using System.Net.Http.Headers;
using System.Text;
using EdmundVehicle.Models;
using EdmundVehicle.Services;
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
    public class VehicleController : ApiController
    {
        private readonly IVehicleService service;

		public VehicleController(IVehicleService service)
		{
		    this.service = service;
		}

        public IEnumerable<Make> UpdateDatabaseFromEdmund()
        {
            service.UpdateVehicleDatabase();
            return null;
        }
    }
}
