using PropertiesAPI.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace PropertiesAPI.Repository
{
    public interface ICachedRepository : IRepository<Property>
    {
        List<Property> GetAll(SearchModel model);
    }
}
