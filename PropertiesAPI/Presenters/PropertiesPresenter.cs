using PropertiesAPI.Models;
using PropertiesAPI.Services.Services;
using PropertiesAPI.Contracts.Entities;
using System.Linq;
using System.Collections.Generic;

namespace PropertiesAPI.Presenters
{
    public class PropertiesPresenter : IPropertiesPresenter
    {
        private readonly IPropertiesService _service;

        public PropertiesPresenter(IPropertiesService service)
        {
            _service = service;
        }

        public void CreateProperty(PropertyModel model)
        {
            _service.CreateProperty(new Property()
            {
                Address = model.Address,
                Name = model.Name,
                Price = model.Price.Value,
                PropertyDescription = model.PropertyDescription,
                Id = model.Id
            });
        }

        public void DeleteProperty(int id)
        {
            _service.DeleteProperty(id);
        }

        public IList<PropertyModel> GetProperties(PropertySearchModel model)
        {
            var properties = _service.GetProperties(new SearchModel() { Address = model.Adsress , Id = model.Id , PriceMax= model.PriceMax , PriceMin= model.PriceMin  });

            return properties.Select(x => new PropertyModel() { Address = x.Address, Id= x.Id, Name=x.Name, Price=x.Price, PropertyDescription=x.PropertyDescription }).ToList();
        }

        public void UpdateProperty(PropertyModel model)
        {
            _service.UpdateProperty(new Property()
            {
                Address = model.Address,
                Name = model.Name,
                Price = model.Price.Value,
                PropertyDescription = model.PropertyDescription,
                Id = model.Id
            });
        }
    }
}