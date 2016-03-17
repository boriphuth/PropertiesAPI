using PropertiesAPI.Models;
using System.Collections.Generic;

namespace PropertiesAPI.Presenters
{
    public interface IPropertiesPresenter
    {
        void CreateProperty(PropertyModel model);

        void UpdateProperty(PropertyModel model);

        void DeleteProperty(int id);

        IList<PropertyModel> GetProperties(PropertySearchModel model);
    }
}
