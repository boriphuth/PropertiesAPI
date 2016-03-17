using System;
using PropertiesAPI.Repository;
using PropertiesAPI.Contracts.Entities;
using System.Collections.Generic;

namespace PropertiesAPI.Services.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly ICachedRepository _repository;

        public PropertiesService(ICachedRepository repository)
        {
            _repository = repository;
        }

        public void CreateProperty(Property property)
        {
            _repository.CreateEntity(property);
        }

        public void DeleteProperty(int id)
        {
            _repository.Delete(id);
        }

        public IList<Property> GetProperties()
        {
            return _repository.GetAll();
        }

        public IList<Property> GetProperties(SearchModel model)
        {
            return _repository.GetAll(model);
        }


        public void UpdateProperty(Property property)
        {
            _repository.Update(property);
        }
    }
}
