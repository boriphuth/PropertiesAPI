using System;
using System.Collections.Generic;
using PropertiesAPI.Contracts.Entities;
using System.Runtime.Caching;
using System.Linq;

namespace PropertiesAPI.Repository
{
    public class CachedRepository: ICachedRepository
    {
        private ObjectCache _cache = MemoryCache.Default;
        private CacheItemPolicy _policy;
        private List<Property> _properties;

        public CachedRepository()
        {
            _policy = new CacheItemPolicy();
            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
            _properties = new List<Property>();
        }  
        
        public void CreateEntity(Property entity)
        {
            _properties.Add(entity);
            _cache.Add("Property", _properties, _policy);
        }

        public void Delete(int id)
        {
            var lists = _cache.Get("Property") as List<Property>;
            var property = lists.Where(x => x.Id == id).FirstOrDefault();
            lists.Remove(property);
        }

        public List<Property> GetAll()
        {
            return _cache.Get("Property") as List<Property>;
        }

        public List<Property> GetAll(SearchModel entity)
        {

            var cachedProperties = _cache.Get("Property") as List<Property>;
            return cachedProperties.Where(x => (entity.Id!=0 ? x.Id == entity.Id : true) &&
                                                (!string.IsNullOrEmpty(entity.Address) ? x.Address == entity.Address : true) && 
                                                ((entity.PriceMin.HasValue && !entity.PriceMax.HasValue) ? ( x.Price > entity.PriceMin && entity.PriceMax < entity.PriceMax) : true))
                                                .ToList();
        }

        public void Update(Property entity)
        {
            var lists = _cache.Get("Property") as List<Property>;
            var property = lists.Where(x => x.Id == entity.Id).FirstOrDefault();
            property.Address = entity.Address;
            property.Name = entity.Name;
            property.Price = entity.Price;
            property.PropertyDescription = entity.PropertyDescription;
            lists.Remove(property);
            lists.Add(property);
        }
    }
}
