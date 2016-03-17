using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesAPI.Contracts.Entities
{
    public class SearchModel : IEntity
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public string Address { get; set; }

        public int Id { get; set; }
    }
}
