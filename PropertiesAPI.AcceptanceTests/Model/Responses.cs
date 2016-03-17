using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesAPI.AcceptanceTests.Model
{
    public class Responses<T> where T : class
    {
        public T Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
