using System;
using System.Collections.Generic;
using PropertiesAPI.Contracts.Entities;
using RestSharp;
using PropertiesAPI.Lib.Helpers;

namespace PropertiesAPI.GatewayAPI.GatewayAPI
{
    public class GatewayAPI : IGatewayAPI
    {
        public void CreateProperty(Property property)
        {
            var request = new HttpRequestWrapper()
                        .SetMethod(Method.POST)
                        .SetResourse("/api/properties/")
                        .AddJsonContent(property);

            var response = request.Execute();
        }

        public void DeleteProperty(int id)
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.DELETE)
                               .SetResourse("/api/properties/")
                               .AddParameter("id", id);

            var response = request.Execute();
        }

        public IList<Property> GetPaginatedProperties(int page, int pageSize)
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.GET)
                               .SetResourse("/api/properties/")
                               .AddParameters(new Dictionary<string, object>() {
                                                                                    { "page", page },
                                                                                    { "pageSize", pageSize }
                                                                               });

          var properties = request.Execute<List<Property>>();
          return properties;

        }

        public void UpdateProperty(Property property)
        {
            var request = new HttpRequestWrapper()
                       .SetMethod(Method.PUT)
                       .SetResourse("/api/properties/")
                       .AddJsonContent(property);

            var response = request.Execute();
        }
    }
}
