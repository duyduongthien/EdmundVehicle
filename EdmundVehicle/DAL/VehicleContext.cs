using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EdmundVehicle.Models;

namespace EdmundVehicle.DAL
{
    public class VehicleContext :DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}