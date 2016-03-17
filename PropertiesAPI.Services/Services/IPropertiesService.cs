using PropertiesAPI.Contracts.Entities;
using System.Collections.Generic;

namespace PropertiesAPI.Services.Services
{
    public interface IPropertiesService
    {
        void CreateProperty(Property property);

        void UpdateProperty(Property property);

        void DeleteProperty(int id);

        IList<Property> GetProperties();

        IList<Property> GetProperties(SearchModel model);
    }
}
