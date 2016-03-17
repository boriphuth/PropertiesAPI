using Newtonsoft.Json;
using NUnit.Framework;
using PropertiesAPI.AcceptanceTests.Helpers;
using PropertiesAPI.AcceptanceTests.Model.Entities;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;
using System.Linq;

namespace PropertiesAPI.AcceptanceTests.Steps
{
    [Binding]
    public class PropertiesSteps 
    {
        private IRestResponse _restResponse;
        private Property _property;
        private HttpStatusCode _statusCode;
        private List<Property> _properties;
      
         [Given(@"I create a new property \((.*),(.*),(.*),(.*),(.*)\)")]
        public void GivenICreateANewProperty(string Address, decimal Price, string Name, string PropertyDescription, int Id)
        {
            _property = new Property()
            {
                Address = Address,
                Name = Name,
                Price = Price,
                PropertyDescription = PropertyDescription,
                Id = Id
            };

            var request = new HttpRequestWrapper()
                            .SetMethod(Method.POST)
                            .SetResourse("/api/properties/")
                            .AddJsonContent(_property);

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;

            ScenarioContext.Current.Add("Pro", _property);
        }

        [Given(@"ModelState is correct")]
        public void GivenModelStateIsCorrect()
        {
            Assert.That(() => !string.IsNullOrEmpty(_property.Address));
            Assert.That(() => !string.IsNullOrEmpty(_property.Name));
            Assert.That(() => !string.IsNullOrEmpty(_property.PropertyDescription));
            Assert.That(() => _property.Price.HasValue);
        }

        [Then(@"the system should return properties that match my criteria")]
        public void ThenTheSystemShouldReturn()
        {
            Assert.AreEqual(_statusCode, HttpStatusCode.Created);
        }


        [Given(@"I request to view properties with pagination \((.*),(.*),(.*),(.*),(.*),(.*)\)")]
        [When(@"I request to view properties with pagination \((.*),(.*),(.*),(.*),(.*),(.*)\)")]
        [Then(@"I request to view properties with pagination \((.*),(.*),(.*),(.*),(.*),(.*)\)")]
        public void GivenIRequestToViewPropertiesWithPagination(int page, int pageSize, string address, decimal priceMin, decimal priceMax, int Id)
        {
            _property = ScenarioContext.Current.Get<Property>("Pro");

            var request = new HttpRequestWrapper()
                               .SetMethod(Method.GET)
                               .SetResourse("/api/properties/")
                               .AddParameters(new Dictionary<string, object>() {
                                                                                    { "Page", page },
                                                                                    { "PageSize", pageSize },
                                                                                    { "PriceMin", priceMin },
                                                                                    { "PriceMax", priceMax },
                                                                                    { "Address",  address },
                                                                                    { "Id",  _property.Id },
                                                                               });

            _restResponse = new RestResponse();
            _restResponse = request.Execute();
          
            _statusCode = _restResponse.StatusCode;
            _properties = JsonConvert.DeserializeObject<List<Property>>(_restResponse.Content);
        }

        [When(@"I delete a property")]
        public void WhenIDeleteAProperty()
        {
            _property = ScenarioContext.Current.Get<Property>("Pro");

            var request = new HttpRequestWrapper()
                               .SetMethod(Method.DELETE)
                               .SetResourse("/api/properties/")
                               .AddParameter("id", _property.Id);

            var response = request.Execute();
        }

        [When(@"I request the same property")]
        public void WhenIRequestTheSameProperty()
        {
            var request = new HttpRequestWrapper()
                               .SetMethod(Method.GET)
                               .SetResourse("/api/properties/")
                               .AddParameter("id", _property.Id);

            _properties = new List<Property>();
            _properties = request.Execute<List<Property>>();
        }
        
        [Then(@"the system should return (.*)")]
        public void ThenTheSystemShouldReturn(HttpStatusCode statusCode)
        {
            Assert.AreEqual(_statusCode, statusCode);
        }
        
        [Then(@"the system should not return any results")]
        public void ThenTheSystemShouldNotReturnAnyResults()
        {
            Assert.That(() => _properties.Count <= 0);
        }
        
        [Then(@"system should return Pagination Headers")]
        public void ThenSystemShouldReturnPaginationHeaders()
        {
            var headers = _restResponse.Headers.Select(x => x.Name == "X-Pagination");

            Assert.That(() => headers.Count() > 0);
        }

        [When(@"I update an existing property \((.*),(.*)\)")]
        public void WhenIUpdateAnExistingProperty(string newAddress, decimal newPrice)
        {
            _property.Address = newAddress;
            _property.Price = newPrice;

            var request = new HttpRequestWrapper()
                            .SetMethod(Method.PUT)
                            .SetResourse("/api/properties/")
                            .AddJsonContent(_property);

            //_restResponse = new RestResponse();
            var response = request.Execute();
        }

        [Then(@"the updated property should be included in the list")]
        public void ThenTheUpdatedPropertyShouldBeIncludedInTheList()
        {
            Assert.That(() => _properties.Contains(_property));
        }

        [Then(@"the server should assign an Etag to the response")]
        public void ThenTheServerShouldAssignAnEtagToTheResponse()
        {
            var headers = _restResponse.Headers.Select(x => x.Name == "ETag");

            Assert.That(() => headers.Count() > 0);
        }

        [When(@"I request to view properties")]
        public void WhenIRequestToViewProperties()
        {
            var header = _restResponse.Headers.Where(x => x.Name == "ETag").First().Value;
            
            var request = new HttpRequestWrapper()
                             .SetMethod(Method.GET)
                             .SetResourse("/api/properties/")
                             .AddEtagHeader(header.ToString())
                             .AddParameters(new Dictionary<string, object>() {
                                                                                    { "page", 1 },
                                                                                    { "pageSize", 11 }
                                                                             });

            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;
            _properties = JsonConvert.DeserializeObject<List<Property>>(_restResponse.Content);
        }

    }
}
