using System.Collections.Generic;

namespace EdmundVehicle.Models
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }

    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdMake { get; set; }
    }
}