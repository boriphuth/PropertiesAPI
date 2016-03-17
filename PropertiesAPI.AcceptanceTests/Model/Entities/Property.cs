using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesAPI.AcceptanceTests.Model.Entities
{
    public class Property
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal? Price { get; set; }

        public string Name { get; set; }

        public string PropertyDescription { get; set; }
    }
}
