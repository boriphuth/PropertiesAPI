using PropertiesAPI.Contracts.Entities;
using System.Collections.Generic;


namespace PropertiesAPI.GatewayAPI.GatewayAPI
{
    public interface IGatewayAPI
    {
        void CreateProperty(Property property);

        void DeleteProperty(int id);

        void UpdateProperty(Property property);

        IList<Property> GetPaginatedProperties(int page, int pageSize);
    }
}
