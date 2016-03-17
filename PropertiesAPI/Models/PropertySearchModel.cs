using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertiesAPI.Models
{
    public class PropertySearchModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public string Adsress { get; set; }

        public int Id { get; set; }
    }
}