using Castle.MicroKernel.Registration;
using Newtonsoft.Json;
using PropertiesAPI.Models;
using PropertiesAPI.Presenters;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Linq;
using System.Net.Http;
using PropertiesAPI.Filters;

namespace PropertiesAPI.Controllers
{
    [RoutePrefix("api")]
    public class PropertiesController : ApiController
    {
        private readonly IPropertiesPresenter _propertiesPresenter;

        public PropertiesController(IPropertiesPresenter propertiesPresenter)
        {
            _propertiesPresenter = propertiesPresenter;
        }

        [HttpPost]
        public IHttpActionResult Post(PropertyModel property)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _propertiesPresenter.CreateProperty(property);
            return ResponseMessage(new HttpResponseMessage
            {
                ReasonPhrase = "Property has been created",
                StatusCode = HttpStatusCode.Created
            });
        }
    
        [HttpPut]
        public IHttpActionResult Put(PropertyModel property)
        {
            _propertiesPresenter.UpdateProperty(property);
            return Ok();
        }

        [EnableETag]
        [HttpGet]
        public IHttpActionResult Get([FromUri]PropertySearchModel model)
        {
            var properties = _propertiesPresenter.GetProperties(model);
            var totalCount = properties.Count; ;
            var totalPages = (int)Math.Ceiling((double)totalCount /model.PageSize);

            var urlHelper = new UrlHelper(Request);

            var prevLink = model.Page > 0 ? Url.Link("DefaultApi", new { controller = "Properties", page = model.Page - 1 }) : "";
            var nextLink = model.Page < totalPages - 1 ? Url.Link("DefaultApi", new { controller = "Properties", page = model.Page + 1 }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrePageLink = prevLink,
                NextPageLink = nextLink
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            var results = properties
            .Skip(model.PageSize * model.Page)
            .Take(model.PageSize)
            .ToList();

            //Results
            return Ok(results);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _propertiesPresenter.DeleteProperty(id);
            return Ok();
        }
    }
}
